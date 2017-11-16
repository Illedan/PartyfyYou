//
//  AuthHandler.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 16/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "AppConfig.h"
#import "ViewController.h"

@interface AuthHandler : NSObject
- (id)initWithAppConfig:(AppConfig*)appConfig viewController:(ViewController*)viewController;
- (void)ensureAuthenticated;
@end
