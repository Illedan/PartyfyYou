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

@interface ViewController : UIViewController
@property (nonatomic, strong) AppConfig *appConfig;
@property (nonatomic, strong) NSTimer *spotifyRefreshTimer;

- (void)authenticationCompleted:(SPTSession*) session;
@end

