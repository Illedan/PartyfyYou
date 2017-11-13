//
//  AppConfig.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface AppConfig : JSONModel
@property (nonatomic, strong) NSString *SpotifyClientId;
@property (nonatomic, strong) NSString *SpotifyClientSecret;
@property (nonatomic, strong) NSString *YouTubeServiceId;
@property (nonatomic, strong) NSString *ServiceDiscoveryURL;
@property (nonatomic, strong) NSString *apiURL;
@end

