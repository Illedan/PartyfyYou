//
//  ViewController.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 10/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "ViewController.h"
#import <XCDYouTubeKit/XCDYouTubeKit.h>

@interface ViewController ()

@end


// Skaffe token fra Spotify ( gå mot url og få bruker til å logge på)
// TODO: Spør API etter hvilken spotifysang som spilles (hvert sekund bruker vi)
// TODO: Spør API etter hva youtube url er om spotifySang er ny (da får man ID til sang som legges etter youtube.com/watch?ID=... (el noe sånt)
// TODO: Spør API etter ny token før expiration period er over (1 time per token)
@implementation ViewController

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

- (void)authenticationCompleted:(SPTSession*) session {
    //[self playVideo];
}

- (void) playVideo
{
    XCDYouTubeVideoPlayerViewController *videoPlayerViewController = [[XCDYouTubeVideoPlayerViewController alloc] initWithVideoIdentifier:@"9bZkp7q19f0"];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(moviePlayerPlaybackDidFinish:) name:MPMoviePlayerPlaybackDidFinishNotification object:videoPlayerViewController.moviePlayer];
    [self presentMoviePlayerViewControllerAnimated:videoPlayerViewController];
}

- (void) moviePlayerPlaybackDidFinish:(NSNotification *)notification
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:MPMoviePlayerPlaybackDidFinishNotification object:notification.object];
    MPMovieFinishReason finishReason = [notification.userInfo[MPMoviePlayerPlaybackDidFinishReasonUserInfoKey] integerValue];
    if (finishReason == MPMovieFinishReasonPlaybackError)
    {
        NSError *error = notification.userInfo[XCDMoviePlayerPlaybackDidFinishErrorUserInfoKey];
        // Handle error
    }
}

@end
