//
//  Service.h
//  PartyFY
//
//  Created by Runar Ovesen Hjerpbakk on 12/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <JSONModel/JSONModel.h>

@interface Service : JSONModel
@property (nonatomic, strong) NSString *name;
@property (nonatomic, strong) NSString *ip;
@end
