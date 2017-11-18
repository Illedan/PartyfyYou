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
#import <RGLockbox/RGLockbox.h>
#import "SpotifySession.h"

@interface AuthHandler ()
@property (strong, nonatomic) AppConfig *appConfig;
@property (strong, nonatomic) ViewController *viewController;
@property (nonatomic, strong) NSTimer *activationRetryTimer;
@property (strong, nonatomic) NSString *activationCode;
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
    
    RGLockbox* lockbox = [RGLockbox manager];
    NSData* spotifyTokenAsData = [lockbox dataForKey:@"spotifyToken"];
    if (spotifyTokenAsData) {
        NSString* spotifyToken = [[NSString alloc] initWithData:spotifyTokenAsData encoding:NSUTF8StringEncoding];
        [self.viewController authenticationCompleted:spotifyToken];
        return;
    }
    
    // TODO: Generate code and show on screen for website auth
    // TODO: Naming
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    [RESTClient get:self.appConfig.authURL responseHandler:^(NSString *oneTimeCode) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        // TODO: Tja, kanskje skrive feilmelding istedenfor å krasje appen
        OneTimeCode* authCode = [[OneTimeCode alloc] initWithString:oneTimeCode error:&error];
        if (error) {
            dispatch_async(dispatch_get_main_queue(), ^{
                [self.viewController couldNotContactServer];
            });
        } else {
            if (!authCode) {
                NSException* myException = [NSException
                                            exceptionWithName:@"OneTimeCodeNotCreated"
                                            reason:@"OneTimeCode could not be constructed from server response"
                                            userInfo:nil];
                [myException raise];
            }
            
            self.activationCode = authCode.code;
            dispatch_async(dispatch_get_main_queue(), ^{
                [self.viewController showAuthCode:authCode];
                // TODO: Push message to app for app auth
                self.activationRetryTimer = [NSTimer scheduledTimerWithTimeInterval:1.0
                                                                             target:self
                                                                           selector:@selector(getSpotifySession:)
                                                                           userInfo:nil
                                                                            repeats:YES];
            });
        }
        
        dispatch_semaphore_signal(sema);
    }];
    dispatch_semaphore_wait(sema, DISPATCH_TIME_FOREVER);
    
    
    
    
    // TODO: Lagre token
//    NSData* data = [@"abcd" dataWithEncoding:NSUTF8StringEncoding];
//    RGLockbox* lockbox = [RGLockbox manager];
//    [lockbox setData:data forKey:@"myData"];
}





- (void)getSpotifySession:(NSTimer*)timer {
    NSString* url = [self.appConfig.authURL stringByAppendingString:self.activationCode];
    [RESTClient get:url responseHandler:^(NSString *token) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        if(!token || [token isEqualToString:@""]) {
            return;
        }
        
        //         TODO: token is wrong name
        SpotifySession* spotifySession = [[SpotifySession alloc] initWithString:token error:&error];
        if (error) {
            // TODO: Errorhandling
        }
        
        if (!spotifySession.access_token) {
            // TODO: Die horribly
        }
        
        [timer invalidate];
        dispatch_async(dispatch_get_main_queue(), ^{
            [self.viewController authenticationCompleted:spotifySession.access_token];
        });
    }];

}


@end
