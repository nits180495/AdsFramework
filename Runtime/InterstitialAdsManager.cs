using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class InterstitialAdsManager
    {
        //variable to hold interstitialAd.
        private InterstitialAd interstitialAd;

        /// <summary>
        /// Loads the interstitial ad with the given adsUnitID.
        /// </summary>
        /// <param name="adsUnitID"></param>
        public void LoadInterstitialAd(string adsUnitID)
        {
            // Clean up the old ad before loading a new one.
            if (interstitialAd != null)
            {
                DestroyAd();
            }

            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            InterstitialAd.Load(adsUnitID, _adRequest,
                (InterstitialAd _ad, LoadAdError _error) =>
                {
                    // if error is not null, the load request failed.
                    if (_error != null || _ad == null)
                    {
                        //show error message
                        return;
                    }

                    interstitialAd = _ad;

                    //Adding Listners for Interstitial Ads
                    AddListnersToInterstitialView(interstitialAd);
                });
        }

        /// <summary>
        /// Shows the interstitial ad if it is already loaded.
        /// </summary>
        public void ShowInterstitialAd()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                interstitialAd.Show();
            }
            else
            {
                //Interstitial ad is not ready yet.
            }
        }

        /// <summary>
        /// Add listeners to events for Interstitial Ads.
        /// </summary>
        /// <param name="ad"></param>
        private void AddListnersToInterstitialView(InterstitialAd ad)
        {
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                if (AdsEvents.OnInterstitialAdClickedEvent != null)
                    AdsEvents.OnInterstitialAdClickedEvent.Invoke();
            };

            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                if (AdsEvents.OnInterstitialAdOpeningEvent != null)
                    AdsEvents.OnInterstitialAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                if (AdsEvents.OnInterstitialAdClosedEvent != null)
                    AdsEvents.OnInterstitialAdClosedEvent.Invoke();
            };

            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                if (AdsEvents.OnInterstitialAdFailedToShowEvent != null)
                    AdsEvents.OnInterstitialAdFailedToShowEvent.Invoke();
            };
        }

        /// <summary>
        /// Destroys the ad if it is not null.
        /// </summary>
        public void DestroyAd()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
                interstitialAd = null;
            }
        }
    }
}