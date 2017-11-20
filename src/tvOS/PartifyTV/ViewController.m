//
//  ViewController.m
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 14/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "ViewController.h"
#import <AVKit/AVKit.h>
#import <XCDYouTubeKit/XCDYouTubeKit.h>
#import "RESTClient.h"
#import "AuthHandler.h"

@interface ViewController ()
@property (nonatomic, strong) NSTimer *spotifyRefreshTimer;
@property (nonatomic, strong) AuthHandler *authHandler;
@property (nonatomic, strong) NSString *currentSpotifyId;
@property (nonatomic, strong) AVPlayerViewController *playerViewController;
@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
    // TODO: Fast størrelse på knappen
}


- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
    
    [self stopTimers];
    self.currentSpotifyId = nil;
    [self.button setEnabled:YES];
    
    self.authHandler = [[AuthHandler alloc] initWithAppConfig:self.appConfig viewController:self];
    if (!self.appConfig.authURL || !self.appConfig.apiURL) {
        
        dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
        [self.authHandler setServicesFromServiceDiscovery:semaphore];
        dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        
        if (!self.appConfig.authURL || !self.appConfig.apiURL) {
            [self showErrorMessage:@"Could not connect to Partify server"];
            return;
        }   
    }
    
    if ([self.authHandler isAlreadyAuthenticated]) {
        [self.button setTitle:@"Partify" forState:UIControlStateNormal];
        self.headerLabel.text = @"Prepare to be Partified!";
        self.codeLabel.text = @"Press Partify to show the music video for the currently playing song on Spotify.";
    } else {
        [self.button setTitle:@"Authenticate" forState:UIControlStateNormal];
        self.headerLabel.text = @"Prepare to be Partified!";
        self.codeLabel.text = @"Press Authenticate to authenticate with Spotify";
    }
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
    
//    self.image.transform = CGAffineTransformMakeScale(0.1, 0.1);
//    [UIView animateWithDuration:5 animations:^(){
//                        self.image.transform = CGAffineTransformMakeScale(1.0, 1.0);
//                        self.image.center = self.view.center;
//                    }
//                    completion:nil];
//
//    self.headerLabel.transform = CGAffineTransformMakeScale(0.1, 0.1);
//    [UIView animateWithDuration:5 animations:^(){
//                        self.headerLabel.transform = CGAffineTransformMakeScale(1.0, 1.0);
//                        self.headerLabel.center = self.headerLabel.center;
//                    }
//                    completion:nil];
}

- (void)showErrorMessage:(NSString*)errorMessage {
    self.headerLabel.text = errorMessage;
    self.codeLabel.text = @"Party is over 🤯";
    [self.button setTitle:@"Try again" forState:UIControlStateNormal];
}

- (void)showOneTimeAuthenticationCode:(OneTimeCode*) authCode {
    self.headerLabel.text = authCode.url;
    self.codeLabel.text = authCode.code;
}

- (void)authenticationCompleted {
    [self playVideoForCurrentlyPlayingSpotifySong:nil];
    self.spotifyRefreshTimer = [NSTimer scheduledTimerWithTimeInterval:1.0
                                                                target:self
                                                              selector:@selector(playVideoForCurrentlyPlayingSpotifySong:)
                                                              userInfo:nil
                                                               repeats:YES];
}

- (void)playVideoForCurrentlyPlayingSpotifySong:(NSTimer*)timer {
    [self getSpotifySongId];
}

// TODO: Use proper service when available...
- (void)getSpotifySongId {
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/id?token=%@", self.appConfig.apiURL, self.spotifySession.access_token];
    [RESTClient get:getCurrentSongURL responseHandler:^(NSString *spotifyId) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }

        [self getYouTubeSongId:spotifyId];
    }];
}

- (void)getYouTubeSongId:(NSString*) spotifyId {
    if (!spotifyId || [spotifyId isEqualToString:@""]) {
        dispatch_async(dispatch_get_main_queue(), ^{
            [self dismissViewControllerAnimated:YES completion:nil];
            [self viewWillAppear:YES];
        });
        return;
    }
    
    if ([self.currentSpotifyId isEqualToString:spotifyId]) {
        return;
    }
    
    self.currentSpotifyId = spotifyId;
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/url?token=%@", self.appConfig.apiURL, self.spotifySession.access_token];
    [RESTClient get:getCurrentSongURL responseHandler:^(NSString *youTubeId) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        // TODO: hva skjer når video ikke kommer etter man trykker på knappen etter å ha vært innom appens åpningsskjerm?
        dispatch_async(dispatch_get_main_queue(), ^{
            if (!self.playerViewController) {
                self.playerViewController = [AVPlayerViewController new];
            }
                
            if (!self.playerViewController.isFirstResponder) {
                [self presentViewController:self.playerViewController animated:YES completion:nil];
            }
            
            __weak AVPlayerViewController *weakPlayerViewController = self.playerViewController;
            [[XCDYouTubeClient defaultClient] getVideoWithIdentifier:youTubeId completionHandler:^(XCDYouTubeVideo * _Nullable video, NSError * _Nullable error) {
                if (video)
                {
                    // TODO: Need to be as HD as possible
                    NSDictionary *streamURLs = video.streamURLs;
                    NSURL *streamURL = streamURLs[XCDYouTubeVideoQualityHTTPLiveStreaming] ?: streamURLs[@(XCDYouTubeVideoQualityHD720)] ?: streamURLs[@(XCDYouTubeVideoQualityMedium360)] ?: streamURLs[@(XCDYouTubeVideoQualitySmall240)];
                    weakPlayerViewController.player = [AVPlayer playerWithURL:streamURL];
                    [weakPlayerViewController.player play];
                }
                else
                {
                    [self dismissViewControllerAnimated:YES completion:nil];
                }
            }];
            
            
        });
        
    }];
}

//- (void)prepareToPlayNewVideo:(NSString*) youTubeId {
//    videoPlayerViewController.videoIdentifier = youTubeId;
//    if (!videoPlayerViewController.isFirstResponder) {
//        [self presentMoviePlayerViewControllerAnimated:videoPlayerViewController];
//    }
//
//    // TODO: Må nok gjøre noe mer. Funker stort sett, men ikke alltid. Stoppe og starte osv?
//    [videoPlayerViewController.moviePlayer prepareToPlay];
//}

- (IBAction)buttonPressed:(UIButton *)sender {
    [self.button setEnabled:NO];
    if ([self.button.currentTitle isEqualToString:@"Authenticate"]) {
        [self.button setTitle:@"Cancel" forState:UIControlStateNormal];
        [self.button setEnabled:YES];
        [self.authHandler ensureAuthenticated];
    } else if ([self.button.currentTitle isEqualToString:@"Partify"]) {
        [self.authHandler ensureAuthenticated];
    } else if ([self.button.currentTitle isEqualToString:@"Cancel"] || [self.button.currentTitle isEqualToString:@"Try again"]) {
        [self viewWillAppear:YES];
    }
    
    
}

- (void)saveSession {
    [self.authHandler saveSession];
}

- (void)stopTimers {
    [self.spotifyRefreshTimer invalidate];
    [self.authHandler.spotifySessionRefreshTimer invalidate];
    [self.authHandler.activationRetryTimer invalidate];
}
@end
