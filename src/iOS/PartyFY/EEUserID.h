//
//  EEUserID.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 14/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface EEUserID : NSObject

+ (NSUUID *) getUUID;
+ (NSString *) getUUIDString;

@end
