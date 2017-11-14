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

@interface ViewController ()

@end

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
    AVPlayerViewController *playerViewController = [AVPlayerViewController new];
    [self presentViewController:playerViewController animated:YES completion:nil];
    
    __weak AVPlayerViewController *weakPlayerViewController = playerViewController;
    [[XCDYouTubeClient defaultClient] getVideoWithIdentifier:@"2ot_katYYiU" completionHandler:^(XCDYouTubeVideo * _Nullable video, NSError * _Nullable error) {
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
}

@end
