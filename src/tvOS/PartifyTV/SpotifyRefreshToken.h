//
//  SpotifyRefreshToken.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 20/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface SpotifyRefreshToken : JSONModel
@property (nonatomic, strong) NSString *access_token;
@property (nonatomic) int expires_in;
@end
