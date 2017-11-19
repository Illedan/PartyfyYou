//
//  AppDelegate.m
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 14/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "AppDelegate.h"
#import "EEUserID.h"
#import "RESTClient.h"
#import "ViewController.h"
#import "Service.h"
#import "AuthHandler.h"

@interface AppDelegate ()
@property (strong, nonatomic) AuthHandler *authHandler;
@property (strong, nonatomic) AppConfig *appConfig;
@end

@implementation AppDelegate

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
    
    NSString* path = [[NSBundle mainBundle] pathForResource:@"config"
                                                     ofType:@"json"];
    
    NSString* myJson = [NSString stringWithContentsOfFile:path
                                                 encoding:NSUTF8StringEncoding
                                                    error:NULL];
    
    NSError *error;
    self.appConfig = [[AppConfig alloc] initWithString:myJson error:&error];
    if (error) {
        NSException* myException = [NSException
                                    exceptionWithName:@"AppConfigNotCreated"
                                    reason:error.localizedFailureReason
                                    userInfo:nil];
        [myException raise];
    }
    
    if (!self.appConfig) {
        NSException* myException = [NSException
                                    exceptionWithName:@"AppConfigNotCreated"
                                    reason:@"AppConfig could not be constructed from config.json"
                                    userInfo:nil];
        [myException raise];
    }
    
    self.appConfig.authURL = nil;
    self.appConfig.apiURL = nil;
    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
    [[(ViewController*)  self.window.rootViewController spotifyRefreshTimer] invalidate];
    [self.authHandler.spotifySessionRefreshTimer invalidate];
    [self.authHandler.activationRetryTimer invalidate];
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    [self.authHandler saveSession];
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the active state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.

    ViewController* mainController = (ViewController*)  self.window.rootViewController;
    if ([self servicesSetFromServiceDiscovery]) {
        mainController.appConfig = self.appConfig;
        // TODO: Gjøre dette hver gang eller bare ved oppstart? kommer vel an på refresh token I guess...
        self.authHandler = [[AuthHandler alloc] initWithAppConfig:self.appConfig viewController:mainController];
        [self.authHandler ensureAuthenticated];
    } else {
        [mainController showErrorMessage:@"Could not connect to Partify server"];
    }
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    [self.authHandler saveSession];
}

- (BOOL)servicesSetFromServiceDiscovery {
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    [RESTClient get:[self.appConfig.ServiceDiscoveryURL stringByAppendingString:@"/partify-service"] responseHandler:^(NSString *response) {
        NSError *error;
        Service* service = [[Service alloc] initWithString:response error:&error];
        if (!error && service) {
            self.appConfig.apiURL = service.ip;
        }
        
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    sema = dispatch_semaphore_create(0);
    [RESTClient get:[self.appConfig.ServiceDiscoveryURL stringByAppendingString:@"/partify-auth-service"] responseHandler:^(NSString *response) {
        NSError *error;
        Service* service = [[Service alloc] initWithString:response error:&error];
        if (!error && service) {
            self.appConfig.authURL = [service.ip stringByAppendingString:@"/activate/code/"];
        }
        
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    if (!self.appConfig.authURL || !self.appConfig.apiURL) {
        return NO;
    }
    
    return YES;
}

@end
