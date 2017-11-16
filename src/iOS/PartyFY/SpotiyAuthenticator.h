//
//  SpotiyAuthenticator.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SafariServices/SafariServices.h>
#import <Spotify/SpotifyAuthentication.h>
#import "AppConfig.h"

@interface SpotiyAuthenticator : NSObject
- (id)initWithConfig:(AppConfig*)config authCompletedHanlder:(void (^) (SPTSession *session))handler;
- (void)startAuthenticationFlow;
- (BOOL)handleCallbackURL:(NSURL*)url;
@end
