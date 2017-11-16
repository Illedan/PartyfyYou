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

@interface AppDelegate ()

@end

@implementation AppDelegate

AppConfig *appConfig;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
    
    NSString* path = [[NSBundle mainBundle] pathForResource:@"config"
                                                     ofType:@"json"];
    
    NSString* myJson = [NSString stringWithContentsOfFile:path
                                                 encoding:NSUTF8StringEncoding
                                                    error:NULL];
    
    NSError *error;
    appConfig = [[AppConfig alloc] initWithString:myJson error:&error];
    if (error) {
        NSException* myException = [NSException
                                    exceptionWithName:@"AppConfigNotCreated"
                                    reason:error.localizedFailureReason
                                    userInfo:nil];
        [myException raise];
    }
    
    if (!appConfig) {
        NSException* myException = [NSException
                                    exceptionWithName:@"AppConfigNotCreated"
                                    reason:@"AppConfig could not be constructed from config.json"
                                    userInfo:nil];
        [myException raise];
    }
    
    return YES;
}


- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}


- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}


- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the active state; here you can undo many of the changes made on entering the background.
}


- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.

    // TODO: Kan gjøres samtidig og med mindre kode
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    [RESTClient get:[appConfig.ServiceDiscoveryURL stringByAppendingString:@"/partify-service"] responseHandler:^(NSString *response) {
        NSError *error;
        Service* service = [[Service alloc] initWithString:response error:&error];
        if (error) {
            // TODO: Errorhandling
        }
        
        if (!service.ip) {
            // TODO: Die horribly
        }
        
        appConfig.apiURL = service.ip;
        
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    sema = dispatch_semaphore_create(0);
    [RESTClient get:[appConfig.ServiceDiscoveryURL stringByAppendingString:@"/partify-auth-service"] responseHandler:^(NSString *response) {
        NSError *error;
        Service* service = [[Service alloc] initWithString:response error:&error];
        if (error) {
            // TODO: Errorhandling
        }
        
        if (!service.ip) {
            // TODO: Die horribly
        }
        
        appConfig.authURL = [service.ip stringByAppendingString:@"/api/auth/"];
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    ViewController* mainController = (ViewController*)  self.window.rootViewController;
    mainController.appConfig = appConfig;
    
    // TODO: Bare gjør dette dersom ikke har token allerede osv
    // TODO: Må skaffe seg refresh token ved behov...
    
    // TODO: Bruk denne på sikt, manuelt må funke først
//    [EEUserID load];
//    NSString *uniqueIDForiTunesAccount = [EEUserID getUUIDString];
    
    // TODO: Naming
    __block NSString* oneTimeKey;
    sema = dispatch_semaphore_create(0);
    [RESTClient get:appConfig.authURL responseHandler:^(NSString *oneTimeToken) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        oneTimeKey = oneTimeToken;
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    
    // TODO: Generate code and show on screen for website auth
    // TODO: Push message to app for app auth
    
    [RESTClient get:[appConfig.authURL stringByAppendingString:oneTimeKey] responseHandler:^(NSString *token) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        dispatch_async(dispatch_get_main_queue(), ^{
            [mainController authenticationCompleted:token];
        });
    }];
}


- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}


@end
