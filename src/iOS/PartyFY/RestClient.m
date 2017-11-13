//
//  RestClient.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 13/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "RestClient.h"

@implementation RestClient
+ (void) makeRestAPICall:(NSString*)url responseHandler:(void (^) (NSString *response))handler {
    NSMutableURLRequest *request = [[NSMutableURLRequest alloc] init];
    [request setURL:[NSURL URLWithString:url]];
    [request setHTTPMethod:@"GET"];
    
    // TODO: Errorhandling would be nice
    NSURLSession *session = [NSURLSession sessionWithConfiguration:[NSURLSessionConfiguration defaultSessionConfiguration]];
    [[session dataTaskWithRequest:request completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
        NSString *requestReply = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        handler(requestReply);
    }] resume];
}
@end
