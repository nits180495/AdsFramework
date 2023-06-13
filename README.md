# AdsFramework
Ads Framework is used for implementing Google Ads.
There are three type of ads that can be implemented by using this framework.
a. Rewards Ads
b Interstitial Ads
c. Banner Ads

## How to use this
You can use this framework by implementing scripts based on your requirements.
The Initialisation is important for any ads to implement.
"AdsInitializer.cs" is used for initialising the mobile ads sdk.
Call Methods to initialise ads and then config test device ids.
Then call the methods to show ads.
You can config App ID's, adsUnit ID's and Test Device ID's.

### Requirements
Install Google Mobile Ads Unity plugin from site or github.
Resolve all dependency and import this framework.
Make sure you set Ads App Id's in Google Mobile Ads Settings.

#### Setup
If you are importing Samples then you will get the Config files in Resources folder which is already created in Samples. Then you can skip creating Scriptable objects steps for config.
Create a "Resources" folder than "AdsFramework" in Assets.
Create Scriptable object by right clicking inside of "AdsFramework" folder, then click on "Create" option on top.
You will see menu on top "AdsFramework", inside option will be there - "AdsConfigData".  
Click to create scriptable object and set config data.
Set test and live Ids according to requirements.
Add "AdsInitializer.cs" script for initialisation of Mobile ads sdk.
Call Initialising method.
Now you can load any ads by calling appropriate methods.
For live/production Ads, please check the Scriptable object "AdsData".
By default it will be Test Ads.

##### Notice
Please properly install Google AdsMob sdk and resolve all dependency. 
For Android 
    If you face any issue regarding jdk or gradle, then try to open preferences and copy the path for jdk. 
    Then uncheck jdk option and paste url in jdk path field. 
    Then try to force resolve dependency.
For iOS
    Make sure you have xcode and all required tool for iOS build.
Also make sure you have added the Ads App ID in google ads settings. 
For banner Set "adKeyword" which is static variable of "BannerAdsManager.cs".
For this go Assets > Google Mobile Ads > Settings.

