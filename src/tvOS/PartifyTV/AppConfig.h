//
//  AppConfig.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 15/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <JSONModel/JSONModel.h>

@interface AppConfig : JSONModel

@property (nonatomic, strong) NSString *ServiceDiscoveryURL;
@property (nonatomic, strong) NSString *apiURL;

@end
