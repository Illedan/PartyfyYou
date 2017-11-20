# Lockbox

[![Build Status](https://travis-ci.org/granoff/Lockbox.png)](https://travis-ci.org/granoff/Lockbox)
[![CocoaPods](https://img.shields.io/cocoapods/p/Lockbox.svg)]()
[![CocoaPods](https://img.shields.io/cocoapods/v/Lockbox.svg)]()
[![CocoaPods](https://img.shields.io/cocoapods/l/Lockbox.svg)]()

Lockbox is an Objective-C utility class for storing data securely in the keychain. Use it to store small, sensitive bits of data securely.

Looking for a Swift version? Check out [Strongbox](https://github.com/granoff/Strongbox).

## Overview

There are some bits of data that an app sometimes needs to store that are sensitive:

+ Usernames
+ Passwords
+ In-App Purchase unlocked feature bits
+ and anything else that, if in the wrong hands, would be B-A-D.

The thing to realize is that data stored in `NSUserDefaults` is stored in the clear! For that matter, most everything stored in your app's sandbox is also there in the clear.

Surprisingly, new and experienced app developers alike often do not realize this, until it's too late.

The Lockbox class methods make it easy to store and retrieve ~`NSString`s, `NSArray`s, `NSSet`s, `NSDictionary`s, and `NSDate`s~ any Foundation-based object that conforms to `NSSecureCoding` into and from the key chain. You are spared having to deal with the keychain APIs directly!

For greater security, and to avoid possible collisions between data stored by your app with data stored by other apps (yours or other developers), the keys you provide in the class methods for storing and retrieving data are prefixed with your app's bundle id. The class methods provide some convenience by simplifying the use of Lockbox. But if you need to be able to access a common set of keys between your app, and say, an iOS8 extension, you may need to override the key prefix. For that, you can instantiate your own instance of Lockbox, providing your custom key prefix, and call the same methods (as instance methods) as you would call on the class. (The signatures are the same between class and instance methods. In fact, the class methods operate on a class-static Lockbox instance.)

The one caveat to keep in mind is that the keychain is really not meant to store large chunks of data, so don't try and store a huge array of data with these APIs simply because you want it secure. In this case, consider alternative encryption techniques.

## Methods

Lockbox 3 includes the following methods, shown here as class methods. The same methods (as instance methods) may be called on your own Lockbox instances.

## General object storage and retrieval

+ `+archiveObject:forKey:`
+ `+archiveObject:forKey:accessibility:`
+ `+unarchiveObject:forKey:`

These methods use an `NSKeyedArchiver` and `NSKeyedUnarchiver`, respectively, to encode and decode your objects. Your objects must conform toe `NSSecureCoding`.

The `+archiveObject:forKey:...` methods return `BOOL` indicating if the keychain operation succeeded or failed. The method `+unarchiveObjectForKey:` method returns a non-`nil` value on success or `nil` on failure. The returned value is of type `id`, but you assign this to whatever you know the data type should be. For example:

```
NSDate *today = [NSDate date];
[Lockbox archiveObject:today forKey:@"theDate"];
...

NSDate *theDate = [Lockbox unarchiveObjectForKey:@"theDate"];
```

See below for notes on the `accessibility` argument.

See below for information about migrating from Lockbox v2.x APIs to Lockbox v3 APIs.

Lockbox 2.x and older include the following deprecated methods, shown here as class methods. The same methods (as instance methods) may be called on your own Lockbox instances.

### NSString (deprecated)

+ `+setString:forKey:`
+ `+setString:forKey:accessibility:`
+ `+stringForKey:`

### NSArray (deprecated)

+ `+setArray:forKey:`
+ `+setArray:forKey:accessibility:`
+ `+arrayForKey:`

### NSSet (deprecated)

+ `+setSet:forKey:`
+ `+setSet:forKey:accessibility:`
+ `+setForKey:`

### NSDictionary (deprecated)

+ `+setDictionary:forKey:`
+ `+setDictionary:forKey:accessibility:`
+ `+dictionaryForKey:`

### NSDate (deprecated)

+ `+setDateForKey:`
+ `+setDateForKey:accessibility:`
+ `+dateForKey:`

All the `setXxx` methods return `BOOL`, indicating if the keychain operation succeeded or failed. The `xxxForKey` methods return a non-`nil` value on success, or `nil` on failure.

The `setXxx` methods will overwrite values for keys that already exist in the keychain, or simply add a keychain entry for the key/value pair if it's not already there.

In all the methods you can use a simple key name, like "MyKey", but know that under the hood Lockbox is prefixing that key with your app's bundle id (if you are using the Class methods, your own key if you are using a Lockbox instance). So the actual key used to store and retrieve the data looks more like "com.mycompany.myapp.MyKey" or "my.custom.key.MyKey". This ensures that your app, and only your app, has access to your data.

The methods with an `accessibility` argument take a [Keychain Item Accessibility
Constant](http://developer.apple.com/library/ios/#DOCUMENTATION/Security/Reference/keychainservices/Reference/reference.html#//apple_ref/doc/uid/TP30000898-CH4g-SW318). You can use this to control when your keychain item should be readable. For
example, passing `kSecAttrAccessibleWhenUnlockedThisDeviceOnly` will make
it accessible only while the device is unlocked, and will not migrate this
item to a new device or installation. The methods without a specific
`accessibility` argument will use `kSecAttrAccessibleWhenUnlocked`, the default in recent iOS versions.

## Migrating to Lockbox v3 APIs

There is no automatic migration from the old `setXxx` methods to the new archive/unarchive methods. In fact, that would be nearly impossible as there is no way to know what you stored for any given key to be able to retrieve the data correctly. The v3 APIs are incompatible with the v2.x APIs.

Manual migration is required. You'll need to fetch your data using the now deprecated type-specific v2.x methods, and then store the data again using the new `archiveObjectForKey:` method.

You might do this at launch, one time, for all your Lockbox data stored using the old methods. Then make a note by storing another key, either via Lockbox or in User Defaults or somewhere else, that you've done this migration in your app. So on subsequent launches, you wouldn't have to do the migration again. In fact, that would be redundant and probably mess up your data.

## ARC Support

As of v2.0, Lockbox is ARC-only. For non-ARC support, use v1.4.9.

## Requirements & Limitations

To use this class you will need to add the `Security` framework to your project.

Your project will have to have Keychain Sharing enabled for Lockbox to access the keychain, but you can remove any Keychain Groups that Xcode adds. The entititlement is apparently required for any keychain access, not just sharing.

This class was written for use under Cocoa Touch and iOS. The code and tests run fine in the iOS simulator under Mac OS. But there are some issues using this class under Cocoa and Mac OS. There are some keychain API differences between the 2 platforms, as it happens. Feel free to fork this repo to make it work for both Cocoa and Cocoa Touch and I'll be happy to consider your pull request!

### Note on running unittests on device
If you experience SecItemCopyMatching errors with code 34018 on Lockbox methods while running your app unit tests target on device, your can avoid these by code signing your unit tests .xcttest folder. 

Add Run Script phase to your unit tests target Build Phases with:

`codesign --verify --force --sign "$CODE_SIGN_IDENTITY" "$CODESIGNING_FOLDER_PATH"`


## Docs
Link to latest CocoaDocs: [cocoadocs.org/docsets/Lockbox/](http://cocoadocs.org/docsets/Lockbox/)  

## License

See the LICENSE file for details.
