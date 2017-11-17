//
//  SpotifySession.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 17/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface SpotifySession : JSONModel
@property (nonatomic, strong) NSString *access_token;
@property (nonatomic) int expires_in;
@property (nonatomic, strong) NSString *refresh_token;
@end
