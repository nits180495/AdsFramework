using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class RewardAdsManager
    {
        //variable to hold rewardedAd.
        private RewardedAd rewardedAd;

        /// <summary>
        /// Loads the rewarded ad with the given adsUnitID.
        /// </summary>
        public void LoadRewardedAd(string adsUnitID)
        {
            // Clean up the old ad before loading a new one.
            if (rewardedAd != null)
            {
                DestroyAd();
            }

            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            RewardedAd.Load(adsUnitID, _adRequest,
                (RewardedAd _ad, LoadAdError _error) =>
                {
                    // if error is not null, the load request failed.
                    if (_error != null || _ad == null)
                    {
                        //Error in loading ad
                        return;
                    }

                    rewardedAd = _ad;

                    //Registering Events for Reward Ads
                    RegisterEventHandlers(rewardedAd);
                });
        }

        /// <summary>
        /// Show Reward ads on screen.
        /// </summary>
        public void ShowRewardedAd()
        {
            const string _REWARD_MSG =
                "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    if (AdsEvents.GrantRewardEvent != null)
                        AdsEvents.GrantRewardEvent.Invoke(_REWARD_MSG, reward.Type, reward.Amount);
                });
            }
        }


        /// <summary>
        /// Method to add listeners to evenets for Interstitial Ads.
        /// </summary>
        /// <param name="ad"></param>
        private void RegisterEventHandlers(RewardedAd ad)
        {
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                if (AdsEvents.OnRewardAdClickedEvent != null)
                    AdsEvents.OnRewardAdClickedEvent.Invoke();
            };

            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                if (AdsEvents.OnRewardAdOpeningEvent != null)
                    AdsEvents.OnRewardAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                if (AdsEvents.OnRewardAdClosedEvent != null)
                    AdsEvents.OnRewardAdClosedEvent.Invoke();
            };

            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                if (AdsEvents.OnRewardAdFailedToShowEvent != null)
                    AdsEvents.OnRewardAdFailedToShowEvent.Invoke();
            };
        }

        /// <summary>
        /// Destroys the ad if it is not null.
        /// </summary>
        public void DestroyAd()
        {
            if (rewardedAd != null)
            {
                rewardedAd?.Destroy();
                rewardedAd = null;
            }
        }
    }
}