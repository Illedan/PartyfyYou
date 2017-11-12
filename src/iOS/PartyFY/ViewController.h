//
//  ViewController.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 10/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <Spotify/SpotifyAuthentication.h>

@interface ViewController : UIViewController
- (void)authenticationCompleted:(SPTSession*) session;
@end

