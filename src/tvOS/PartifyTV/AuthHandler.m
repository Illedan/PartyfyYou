//
//  AuthHandler.m
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 16/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "AuthHandler.h"
#import "RESTClient.h"
#import "OneTimeCode.h"

@interface AuthHandler ()
@property (strong, nonatomic) AppConfig *appConfig;
@property (strong, nonatomic) ViewController *viewController;
@end

@implementation AuthHandler

- (id)initWithAppConfig:(AppConfig*)appConfig viewController:(ViewController*)viewController {
    self.appConfig = appConfig;
    self.viewController = viewController;
    return self;
}

- (void)ensureAuthenticated {
    // TODO: Bare gjør dette dersom ikke har token allerede osv
    // TODO: Må skaffe seg refresh token ved behov...
    
    // TODO: Bruk denne på sikt, manuelt må funke først
    //    [EEUserID load];
    //    NSString *uniqueIDForiTunesAccount = [EEUserID getUUIDString];
    
    
    // TODO: Generate code and show on screen for website auth
    // TODO: Naming
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    [RESTClient get:self.appConfig.authURL responseHandler:^(NSString *oneTimeCode) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        OneTimeCode* authCode = [[OneTimeCode alloc] initWithString:oneTimeCode error:&error];
        if (error) {
            NSException* myException = [NSException
                                        exceptionWithName:@"OneTimeCodeNotCreated"
                                        reason:error.localizedFailureReason
                                        userInfo:nil];
            [myException raise];
        }
        
        if (!authCode) {
            NSException* myException = [NSException
                                        exceptionWithName:@"OneTimeCodeNotCreated"
                                        reason:@"OneTimeCode could not be constructed from server response"
                                        userInfo:nil];
            [myException raise];
        }
        dispatch_async(dispatch_get_main_queue(), ^{
            [self.viewController showAuthCode:authCode];
        });
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    // TODO: Push message to app for app auth
    
    // TODO: loop here untill succesfully authenticated on website...
//    [RESTClient get:[self.appConfig.authURL stringByAppendingString:oneTimeKey] responseHandler:^(NSString *token) {
//        NSError *error;
//        if (error) {
//            // TODO: Errorhandling
//        }
//        dispatch_async(dispatch_get_main_queue(), ^{
//            [self.viewController authenticationCompleted:token];
//        });
//    }];
}

@end
