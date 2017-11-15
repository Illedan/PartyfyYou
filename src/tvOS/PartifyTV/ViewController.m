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

@interface ViewController ()

@end

@implementation ViewController

NSString *currentSession;
NSString *currentSpotifyId;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
}


- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
}

- (void)authenticationCompleted:(NSString*)session {
    currentSession = session;
    
    // TODO: pause video and timer if app is sent to background
    self.spotifyRefreshTimer = [NSTimer scheduledTimerWithTimeInterval:1.0
                                                                target:self
                                                              selector:@selector(playVideoForCurrentlyPlayingSpotifySong:)
                                                              userInfo:nil
                                                               repeats:YES];
    
    // TODO: Why do timer no work
    [self getSpotifySongId];
}

- (void)playVideoForCurrentlyPlayingSpotifySong:(NSTimer*)timer {
    [self getSpotifySongId];
}

// TODO: Change service discovery key
- (void)getSpotifySongId {
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/id?token=%@", self.appConfig.apiURL, currentSession];
    [RESTClient get:getCurrentSongURL responseHandler:^(NSString *spotifyId) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        // TODO: enable background playing and update OS states
        // TODO: Test properly with airplay
        if (![currentSpotifyId isEqualToString:spotifyId]) {
            currentSpotifyId = spotifyId;
            [self getYouTubeSongId:spotifyId];
        }
    }];
}

- (void)getYouTubeSongId:(NSString*) spotifyId {
    // TODO: Use proper service when available...
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/url?token=%@", self.appConfig.apiURL, currentSession];
    [RESTClient get:getCurrentSongURL responseHandler:^(NSString *youTubeId) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        dispatch_async(dispatch_get_main_queue(), ^{
            
            AVPlayerViewController *playerViewController = [AVPlayerViewController new];
            [self presentViewController:playerViewController animated:YES completion:nil];
            
            __weak AVPlayerViewController *weakPlayerViewController = playerViewController;
            [[XCDYouTubeClient defaultClient] getVideoWithIdentifier:youTubeId completionHandler:^(XCDYouTubeVideo * _Nullable video, NSError * _Nullable error) {
                if (video)
                {
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

@end
