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
#import "Service.h"

@interface AuthHandler ()
@property (strong, nonatomic) AppConfig *appConfig;
@property (strong, nonatomic) ViewController *viewController;
@property (strong, nonatomic) NSString *activationCode;
@property (strong, nonatomic) SpotifySession *spotifySession;
@end

@implementation AuthHandler

- (id)initWithAppConfig:(AppConfig*)appConfig viewController:(ViewController*)viewController {
    self.appConfig = appConfig;
    self.viewController = viewController;
    return self;
}

- (void)ensureAuthenticated {
    // TODO: Bruk iCloud på sikt, men manuelt må funke først
    //    [EEUserID load];
    //    NSString *uniqueIDForiTunesAccount = [EEUserID getUUIDString];
    
    // TODO: Push to iOS app for auth
    
    if ([self isAlreadyAuthenticated]) {
        [self refreshSpotifySession:nil];
        [self authenticationCompleted];
        return;
    }
    
    [self getOneTimeCodeAndAuthenticateRemotely];
}

- (BOOL)isAlreadyAuthenticated {
    RGLockbox* lockbox = [RGLockbox manager];
    NSData* sessionAsData = [lockbox dataForKey:@"spotifyToken"];
    NSString* sessionAsString = [[NSString alloc] initWithData:sessionAsData encoding:NSUTF8StringEncoding];
    NSError *error;
    SpotifySession* spotifySession = [[SpotifySession alloc] initWithString:sessionAsString error:&error];
    if (!error && spotifySession) {
        self.spotifySession = spotifySession;
        return YES;
    }
    
    return NO;
}

// TODO: do call and set session afterwards
- (void)refreshSpotifySession:(NSTimer*)timer {
    self.viewController.spotifySession = self.spotifySession;
}

- (void)getOneTimeCodeAndAuthenticateRemotely {
    dispatch_semaphore_t sema = dispatch_semaphore_create(0);
    [RESTClient get:self.appConfig.authURL responseHandler:^(NSString *oneTimeCode) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        OneTimeCode* authCode = [[OneTimeCode alloc] initWithString:oneTimeCode error:&error];
        if (error || !authCode) {
            dispatch_async(dispatch_get_main_queue(), ^{
                [self.viewController showErrorMessage:@"Could not authenticate with Partify server"];
            });
        } else {
            self.activationCode = authCode.code;
            dispatch_async(dispatch_get_main_queue(), ^{
                [self.viewController showOneTimeAuthenticationCode:authCode];
                
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
}

- (void)getSpotifySession:(NSTimer*)timer {
    NSString* url = [self.appConfig.authURL stringByAppendingString:self.activationCode];
    [RESTClient get:url responseHandler:^(NSString *session) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        if(!session || [session isEqualToString:@""]) {
            return;
        }
        
        self.spotifySession = [[SpotifySession alloc] initWithString:session error:&error];
        if (error || self.spotifySession.access_token == nil || [self.spotifySession.access_token isEqualToString:@""] || self.spotifySession.refresh_token == nil || [self.spotifySession.refresh_token isEqualToString:@""]) {
            dispatch_async(dispatch_get_main_queue(), ^{
                [timer invalidate];
                [self.viewController showErrorMessage:@"Could not authenticate with Spotify"];
            });
        } else {
            
            [timer invalidate];
            [self saveSession];
            dispatch_async(dispatch_get_main_queue(), ^{
                [self authenticationCompleted];
            });
        }
    }];
}

- (void)authenticationCompleted {
    self.spotifySessionRefreshTimer = [NSTimer scheduledTimerWithTimeInterval:1.0
                                                                 target:self
                                                               selector:@selector(refreshSpotifySession:)
                                                               userInfo:nil
                                                                repeats:YES];
    self.viewController.spotifySession = self.spotifySession;
    [self.viewController authenticationCompleted];
}

- (void)saveSession {
    if (!self.spotifySession) {
        return;
    }
    
    RGLockbox* lockbox = [RGLockbox manager];
    NSString* sessionAsJSON = [self.spotifySession toJSONString];
    NSData* sessionAsData = [sessionAsJSON dataUsingEncoding:NSUTF8StringEncoding];
    [lockbox setData:sessionAsData forKey:@"spotifyToken"];
}

- (void)setServicesFromServiceDiscovery:(dispatch_semaphore_t)semaphore {
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
    dispatch_semaphore_signal(semaphore);
    
    self.viewController.appConfig = self.appConfig;
}

@end
