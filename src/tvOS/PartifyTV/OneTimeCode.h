//
//  OneTimeCode.h
//  PartifyTV
//
//  Created by Runar Ovesen Hjerpbakk on 16/11/2017.
//  Copyright © 2017 Runar Ovesen Hjerpbakk. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface OneTimeCode : JSONModel
@property (nonatomic, strong) NSString *code;
@property (nonatomic, strong) NSString *url;
@end
