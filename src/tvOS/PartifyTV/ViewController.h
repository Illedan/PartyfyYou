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

@interface ViewController : UIViewController

@property (nonatomic, strong) AppConfig *appConfig;
@property (nonatomic, strong) NSTimer *spotifyRefreshTimer;
@property (weak, nonatomic) IBOutlet UILabel *codeLabel;

@property (weak, nonatomic) IBOutlet UILabel *headerLabel;
@property (weak, nonatomic) IBOutlet UIImageView *image;

- (void)authenticationCompleted:(NSString*) session;
- (void)showAuthCode:(OneTimeCode*) authCode;

@end

