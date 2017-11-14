//
//  SpotiyAuthenticator.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "SpotiyAuthenticator.h"

@interface SpotiyAuthenticator()
@property (nonatomic, strong) SPTAuth *auth;
@property (nonatomic, strong) UIViewController *authViewController;
@property (nonatomic, strong) ViewController *viewController;
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



- (id)initWithConfig:(AppConfig*)config viewController:(ViewController*)viewController {
    self.viewController = viewController;
    self.auth = [SPTAuth defaultInstance];
    // The client ID you got from the developer site
    self.auth.clientID = config.SpotifyClientId;
    // The redirect URL as you entered it at the developer site
    self.auth.redirectURL = [NSURL URLWithString:@"com.hjerpbakk.partyfy.auth://completed/"];
    // Setting the `sessionUserDefaultsKey` enables SPTAuth to automatically store the session object for future use.
    self.auth.sessionUserDefaultsKey = @"current session";
    // Set the scopes you need the user to authorize. `SPTAuthStreamingScope` is required for playing audio.
    self.auth.requestedScopes = @[SPTAuthStreamingScope];
    
    
    // Start authenticating when the app is finished launching
    dispatch_async(dispatch_get_main_queue(), ^{
        [self startAuthenticationFlow];
    });
    
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
        [self.viewController presentViewController:self.authViewController animated:YES completion:nil];
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
    [self.viewController authenticationCompleted:self.auth.session];
}

@end
