//
//  SpotiyAuthenticator.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "SpotiyAuthenticator.h"
#import <Lockbox/Lockbox.h>

@interface SpotiyAuthenticator()
@property (nonatomic, strong) SPTAuth *auth;
@property (nonatomic, strong) UIViewController *authViewController;
@property (nonatomic, strong) void (^authCompletedHanlder)(SPTSession *session);
@end

@implementation SpotiyAuthenticator

// TODO: Lag side i appen med en knapp man må trykke på som igjen kjører denne
// TODO: Lagre session hver gang vi får ny i NSUserDefaults
// TODO: Hent den opp igjen, lag ny om ikke finnes fra før. Kjør flyten under...

//SPTSession *session = …; // Restore session
//
//if (session == nil) {
//    // No session at all - use SPTAuth to ask the user
//    // for access to their account.
//    [self presentFirstTimeLoginToUser];
//
//} else if ([session isValid]) {
//    // Our session is valid - go straight to music playback.
//    [self playMusicWithSession:session];
//
//} else {
//    // Session expired - we need to refresh it before continuing.
//    // This process doesn't involve user interaction unless it fails.
//    NSURL *refreshServiceEndpoint = …;
//    [SPTAuth defaultInstance] renewSession:session
//callback:^(NSError *error, SPTSession *session)
//    {
//        if (error == nil) {
//            [self playMusicWithSession:session];
//        } else {
//            [self handleError:error];
//        }
//    }];
//}

- (id)initWithConfig:(AppConfig*)config authCompletedHanlder:(void (^) (SPTSession *session))handler{
    self.authCompletedHanlder = handler;
    self.auth = [SPTAuth defaultInstance];
    self.auth.clientID = config.SpotifyClientId;
    self.auth.redirectURL = [NSURL URLWithString:@"com.hjerpbakk.partyfy.auth://completed/"];
    // TODO: what scope is needed?
    self.auth.requestedScopes = @[SPTAuthStreamingScope];
    
    self.auth.session = [Lockbox unarchiveObjectForKey:@"SpotifySession"];
    return self;
}

- (void)startAuthenticationFlow {
    // Check if we could use the access token we already have
    if ([self.auth.session isValid]) {
        [self authenticatedWithValidSession];
    } else {
        // Get the URL to the Spotify authorization portal
        NSURL *authURL = [self.auth spotifyWebAuthenticationURL];
        // Present in a SafariViewController
        self.authViewController = [[SFSafariViewController alloc] initWithURL:authURL];
        [[[[UIApplication sharedApplication] keyWindow] rootViewController] presentViewController:self.authViewController animated:YES completion:nil];
    }
}

// Handle auth callback
- (BOOL)handleCallbackURL:(NSURL*)url {
    // If the incoming url is what we expect we handle it
    if ([self.auth canHandleURL:url]) {
        // Close the authentication window
        [self.authViewController.presentingViewController dismissViewControllerAnimated:YES completion:nil];
        self.authViewController = nil;
        // Parse the incoming url to a session object
        [self.auth handleAuthCallbackWithTriggeredAuthURL:url callback:^(NSError *error, SPTSession *session) {
            if (session) {
                [self authenticatedWithValidSession];
            }
        }];
        return YES;
    }
    return NO;
}

- (void)authenticatedWithValidSession {
    [Lockbox archiveObject:self.auth.session forKey:@"SpotifySession"];
    self.authCompletedHanlder(self.auth.session);
}

@end
