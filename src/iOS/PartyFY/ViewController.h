//
//  ViewController.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 10/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <Spotify/SpotifyAuthentication.h>
#import "AppConfig.h"
#import "SpotiyAuthenticator.h"

@interface ViewController : UIViewController
@property (strong, nonatomic) AppConfig *appConfig;
@property (strong, nonatomic) SpotiyAuthenticator *spotifyAuthenticator;

- (IBAction)PlayShowVideoForCurrentSpotifySong:(UIButton *)sender;
- (void)authenticationCompleted:(SPTSession*) session;
- (BOOL)handleURL:(NSURL *)url;
@end

