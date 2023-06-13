using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class AdsEvents
    {
        #region Ads Intialization Events
        //action to  notify for intialization of ads.
        public static Action AdsInitComplete = delegate { };
        #endregion

        #region Reward Ads Events
        //Actions to notify what happend after reward Ad is created.
        public static Action<string, string, double> GrantRewardEvent = delegate { };
        public static Action OnRewardAdOpeningEvent = delegate { };
        public static Action OnRewardAdClickedEvent = delegate { };
        public static Action OnRewardAdFailedToShowEvent = delegate { };
        public static Action OnRewardAdClosedEvent = delegate { };
        #endregion

        #region Banner Ads Events
        //Actions to notify what happend after banner ad is created.
        public static Action OnBannerAdLoadedEvent = delegate { };
        public static Action OnBannerAdOpeningEvent = delegate { };
        public static Action OnBannerAdClickedEvent = delegate { };
        public static Action OnBannerAdFailedToLoadEvent = delegate { };
        public static Action OnBannerAdClosedEvent = delegate { };
        #endregion

        #region Interstitial Ads Events
        //Actions to notify what happend after interstitial Ad is created.
        public static Action OnInterstitialAdOpeningEvent = delegate { };
        public static Action OnInterstitialAdClickedEvent = delegate { };
        public static Action OnInterstitialAdFailedToShowEvent = delegate { };
        public static Action OnInterstitialAdClosedEvent = delegate { };
        #endregion
    }
}
