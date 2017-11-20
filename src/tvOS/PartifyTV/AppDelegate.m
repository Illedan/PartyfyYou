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
@property (strong, nonatomic) ViewController* mainController;
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
    AppConfig* appConfig = [[AppConfig alloc] initWithString:myJson error:&error];
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
    
    appConfig.authURL = nil;
    appConfig.apiURL = nil;
    
    self.mainController = (ViewController*)  self.window.rootViewController;
    self.mainController.appConfig = appConfig;
    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
    [(ViewController*)  self.window.rootViewController stopTimers];
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    [self.mainController saveSession];
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the active state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    [self.mainController saveSession];
}

@end
