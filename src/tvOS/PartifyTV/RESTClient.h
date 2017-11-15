//
//  RestClient.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 15/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface RESTClient : NSObject
+ (void) get:(NSString*)url responseHandler:(void (^) (NSString *response))handler;
+ (void) put:(NSString*)url body:(NSString*)body responseHandler:(void (^) (NSString *response))handler;
@end

