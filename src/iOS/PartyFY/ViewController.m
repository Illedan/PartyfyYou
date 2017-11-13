//
//  ViewController.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 10/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "ViewController.h"
#import <XCDYouTubeKit/XCDYouTubeKit.h>
#import "RestClient.h"

@interface ViewController ()

@end


// Skaffe token fra Spotify ( gå mot url og få bruker til å logge på)
// TODO: Spør API etter hvilken spotifysang som spilles (hvert sekund bruker vi)
// TODO: Spør API etter hva youtube url er om spotifySang er ny (da får man ID til sang som legges etter youtube.com/watch?ID=... (el noe sånt)
// TODO: Spør API etter ny token før expiration period er over (1 time per token)
@implementation ViewController

SPTSession *currentSession;
NSString *currentYouTubeId;
NSTimer *timer;

XCDYouTubeVideoPlayerViewController *videoPlayerViewController;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
    
    videoPlayerViewController = [[XCDYouTubeVideoPlayerViewController alloc] init];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(moviePlayerPlaybackDidFinish:) name:MPMoviePlayerPlaybackDidFinishNotification object:videoPlayerViewController.moviePlayer];
//    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(videoPlayerViewControllerDidReceiveVideo:) name:MPMoviePlayerNowPlayingMovieDidChangeNotification object:videoPlayerViewController.moviePlayer];
}


- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
}

- (void)authenticationCompleted:(SPTSession*)session {
    currentSession = session;
    
    //[self playVideo:@"h7ArUgxtlJs"];
    //[self getSpotifySongId:session];
    
    // TODO: pause video and timer if app is sent to background
    timer = [NSTimer scheduledTimerWithTimeInterval:1.0
                                              target:self
                                            selector:@selector(playVideoForCurrentlyPlayingSpotifySong:)
                                            userInfo:nil
                                             repeats:YES];
}

- (void)playVideoForCurrentlyPlayingSpotifySong:(NSTimer*)timer {
    [self getYouTubeSongId:@""];
}

// TODO: Do I need this?
- (void)getSpotifySongId {
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/id?token=%@", self.appConfig.apiURL, currentSession.accessToken];
    [RestClient makeRestAPICall:getCurrentSongURL responseHandler:^(NSString *response) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        [self getYouTubeSongId:response];
    }];
}

// TODO: Simplify if other method is not needed
- (void)getYouTubeSongId:(NSString*) spotifyId {
    NSString* getCurrentSongURL = [NSString stringWithFormat:@"%@/url?token=%@", self.appConfig.apiURL, currentSession.accessToken];
    [RestClient makeRestAPICall:getCurrentSongURL responseHandler:^(NSString *youTubeId) {
        NSError *error;
        if (error) {
            // TODO: Errorhandling
        }
        
        if ([youTubeId isEqualToString:currentYouTubeId]) {
            dispatch_async(dispatch_get_main_queue(), ^{
                if (videoPlayerViewController.moviePlayer.playbackState != MPNowPlayingPlaybackStatePlaying) {
                    [videoPlayerViewController.moviePlayer play];
                }
            });

            return ;
        }
        
        currentYouTubeId = youTubeId;
        dispatch_async(dispatch_get_main_queue(), ^{
            // TODO: Stop currently playing video. Reuse views and such?
            videoPlayerViewController.videoIdentifier = youTubeId;
            if (!videoPlayerViewController.isFirstResponder) {
                [self presentMoviePlayerViewControllerAnimated:videoPlayerViewController];
            }
            
            [videoPlayerViewController.moviePlayer prepareToPlay];
        });
       
    }];
}

- (void)playVideo:(NSString*) youTubeId {
    XCDYouTubeVideoPlayerViewController* videoPlayerViewController = [[XCDYouTubeVideoPlayerViewController alloc] initWithVideoIdentifier:youTubeId];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(moviePlayerPlaybackDidFinish:) name:MPMoviePlayerPlaybackDidFinishNotification object:videoPlayerViewController.moviePlayer];
    [self presentMoviePlayerViewControllerAnimated:videoPlayerViewController];
}

- (void)moviePlayerPlaybackDidFinish:(NSNotification *)notification
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:MPMoviePlayerPlaybackDidFinishNotification object:notification.object];
    MPMovieFinishReason finishReason = [notification.userInfo[MPMoviePlayerPlaybackDidFinishReasonUserInfoKey] integerValue];
    if (finishReason == MPMovieFinishReasonPlaybackError)
    {
        NSError *error = notification.userInfo[XCDMoviePlayerPlaybackDidFinishErrorUserInfoKey];
        // Handle error
    }
}

- (void) videoPlayerViewControllerDidReceiveVideo:(NSNotification *)notification
{
    dispatch_async(dispatch_get_main_queue(), ^{
        XCDYouTubeVideoPlayerViewController *videoPlayerViewController = notification.object;
        [videoPlayerViewController.moviePlayer prepareToPlay];
    });
}

@end
