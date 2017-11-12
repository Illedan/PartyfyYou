//
//  AppConfig.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface AppConfig : JSONModel
@property (nonatomic) NSString *SpotifyClientId;
@property (nonatomic) NSString *SpotifyClientSecret;
@property (nonatomic) NSString *YouTubeServiceId;
@end
