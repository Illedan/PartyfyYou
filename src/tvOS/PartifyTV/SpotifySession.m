//
//  SpotifySession.m
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 17/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "SpotifySession.h"

@implementation SpotifySession
+ (BOOL)supportsSecureCoding {
    return YES;
}

- (id)initWithCoder:(NSCoder *)coder {
    if ((self = [super init])) {
        self.access_token = [coder decodeObjectOfClass:[NSString class] forKey:@"access_token"];
        self.expires_in = [coder decodeIntForKey:@"expires_in"];
        self.refresh_token = [coder decodeObjectOfClass:[NSString class] forKey:@"refresh_token"];
    }
    return self;
}

- (void)encodeWithCoder:(NSCoder *)coder {
    [coder encodeObject:self.access_token forKey:@"access_token"];
    [coder encodeInt:self.expires_in forKey:@"expires_in"];
    [coder encodeObject:self.refresh_token forKey:@"refresh_token"];
}
@end
