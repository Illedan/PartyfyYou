//
//  RestClient.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 13/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface RestClient : NSObject
+ (void) makeRestAPICall:(NSString*)url responseHandler:(void (^) (NSString *response))handler;
@end
