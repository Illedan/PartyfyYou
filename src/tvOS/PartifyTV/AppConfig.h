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

// TODO: Create another config for the config file... Has 2 unneeded properties in it now
@property (nonatomic, strong) NSString *ServiceDiscoveryURL;
@property (nonatomic, strong) NSString *apiURL;
@property (nonatomic, strong) NSString *authURL;

@end
