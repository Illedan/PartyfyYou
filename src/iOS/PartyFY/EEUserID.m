//
//  EEUserID.m
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 14/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import "EEUserID.h"

@implementation EEUserID
+ (NSUUID *) getUUID
{
    NSUUID *uuid = nil;
    NSString *uuidString = [[NSUbiquitousKeyValueStore defaultStore] stringForKey: @"EEUserID"];
    if (uuidString == nil)
    {
        // This is our first launch for this iTunes account, so we generate random UUID and store it in iCloud:
        uuid = [NSUUID UUID];
        [[NSUbiquitousKeyValueStore defaultStore] setString: uuid.UUIDString forKey: @"EEUserID"];
        [[NSUbiquitousKeyValueStore defaultStore] synchronize];
    }
    else
    {
        uuid = [[NSUUID alloc] initWithUUIDString: uuidString];
    }
    
    return uuid;
}

+ (NSString *) getUUIDString
{
    NSUUID *uuid = [self getUUID];
    if (uuid != nil)
        return uuid.UUIDString;
    else
        return nil;
}

+ (void) load
{
    // get changes that might have happened while this
    // instance of your app wasn't running
    [[NSUbiquitousKeyValueStore defaultStore] synchronize];
}
@end
