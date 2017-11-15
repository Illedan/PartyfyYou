//
//  RestClient.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 13/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "RESTClient.h"

@implementation RESTClient
+ (void) get:(NSString*)url responseHandler:(void (^) (NSString *response))handler {
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

+ (void) put:(NSString*)url body:(NSString*)body responseHandler:(void (^) (NSString *response))handler {
    NSString *jsonBody = [@"\"" stringByAppendingString:body];
    jsonBody = [jsonBody stringByAppendingString:@"\""];
    NSMutableURLRequest *request = [[NSMutableURLRequest alloc] init];
    [request setURL:[NSURL URLWithString:url]];
    [request setHTTPMethod:@"PUT"];
    [request setValue:@"application/json" forHTTPHeaderField:@"Content-type"];
    [request setHTTPBody:[body dataUsingEncoding:NSUTF8StringEncoding]];
    
    // TODO: Errorhandling would be nice
    NSURLSession *session = [NSURLSession sessionWithConfiguration:[NSURLSessionConfiguration defaultSessionConfiguration]];
    [[session dataTaskWithRequest:request completionHandler:^(NSData *data, NSURLResponse *response, NSError *error) {
        NSString *requestReply = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        handler(requestReply);
    }] resume];
}
@end

