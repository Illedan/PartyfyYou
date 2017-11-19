//
//  ViewController.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 14/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppConfig.h"
#import "OneTimeCode.h"
#import "SpotifySession.h"

@interface ViewController : UIViewController

@property (nonatomic, strong) AppConfig *appConfig;
@property (nonatomic, strong) NSTimer *spotifyRefreshTimer;
@property (nonatomic, strong) SpotifySession *spotifySession;

@property (weak, nonatomic) IBOutlet UILabel *codeLabel;
@property (weak, nonatomic) IBOutlet UILabel *headerLabel;
@property (weak, nonatomic) IBOutlet UIImageView *image;
- (IBAction)buttonPressed:(UIButton *)sender;
@property (weak, nonatomic) IBOutlet UIButton *button;

- (void)showErrorMessage:(NSString*)errorMessage;
- (void)authenticationCompleted;
- (void)showOneTimeAuthenticationCode:(OneTimeCode*) authCode;

@end

