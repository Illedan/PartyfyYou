//
//  AppDelegate.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 10/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "AppDelegate.h"
#import "RestClient.h"
#import "Service.h"

@interface AppDelegate ()
@property (nonatomic, strong) SpotiyAuthenticator *spotifyAuthenticator;
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
        return NO;
    }
        
    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and invalidate graphics rendering callbacks. Games should use this method to pause the game.
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
    
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    ViewController* mainController = (ViewController*)  self.window.rootViewController;
    [RestClient makeRestAPICall:appConfig.ServiceDiscoveryURL responseHandler:^(NSString *response) {
        NSError *error;
        Service* service = [[Service alloc] initWithString:response error:&error];
        if (error) {
            // TODO: Errorhandling
        }
        
        appConfig.apiURL = service.ip;
        mainController.appConfig = appConfig;
        dispatch_semaphore_signal(sema);
    }];
    
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    self.spotifyAuthenticator = [[SpotiyAuthenticator alloc] initWithConfig:appConfig viewController:mainController];
}


- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

- (BOOL)application:(UIApplication *)app
            openURL:(NSURL *)url
            options:(NSDictionary *)options
{
    if ([self.spotifyAuthenticator handleCallbackURL:url]) {
        return YES;
    }
    
    return NO;
}

@end
