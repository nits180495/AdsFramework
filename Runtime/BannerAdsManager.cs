using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class BannerAdsManager 
    {
        //variable to hold bannerview.
        private BannerView bannerView;

        /// <summary>
        /// Creates a banner with given banner size at given postion of the screen.
        /// </summary>
        /// <param name="adUnitId"></param>
        /// <param name="bannerSize"></param>
        /// <param name="adPosition"></param>
        public void CreateBannerView
            (string adUnitId, AdSize.Type bannerSize, AdPosition adPosition, int bannerWidth = 0)
        {
            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                DestroyAd();
            }
            bannerView = new BannerView(adUnitId, GetBannerSize(bannerSize, bannerWidth), adPosition);
            AddListnersToBannerView();
        }

        /// <summary>
        /// Creates a banner with given banner size at given coordinates on the screen.
        /// </summary>
        /// <param name="adUnitId"></param>
        /// <param name="bannerSize"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void CreateBannerView(string adUnitId, AdSize.Type bannerSize,int x, int y, int bannerWidth = 0)
        {
            // If we already have a banner, destroy the old one.
            if (bannerView != null)
            {
                DestroyAd();
            }

            bannerView = new BannerView(adUnitId, GetBannerSize(bannerSize, bannerWidth), x, y);

            AddListnersToBannerView();
        }

        /// <summary>
        /// Return AdSize for banner. 
        /// </summary>
        /// <param name="bannerSize"></param>
        /// <returns></returns>
        private AdSize GetBannerSize(AdSize.Type bannerSize, int bannerWidth)
        {
            AdSize _adSize = AdSize.Banner;

            switch (bannerSize)
            {
                case AdSize.Type.Standard:
                    _adSize = AdSize.Banner;
                    break;

                case AdSize.Type.AnchoredAdaptive:
                    _adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(bannerWidth);
                    break;
            }
            return _adSize;
        }

        /// <summary>
        /// Adding Listeners to the events that banner may raise.
        /// </summary>
        private void AddListnersToBannerView()
        {
            // Raised when an ad is loaded into the banner view.
            bannerView.OnBannerAdLoaded += () =>
            {
                if (AdsEvents.OnBannerAdLoadedEvent != null)
                    AdsEvents.OnBannerAdLoadedEvent.Invoke();
            };

            // Raised when an ad fails to load into the banner view.
            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                if (AdsEvents.OnBannerAdFailedToLoadEvent != null)
                    AdsEvents.OnBannerAdFailedToLoadEvent.Invoke();
            };

            // Raised when a click is recorded for an ad.
            bannerView.OnAdClicked += () =>
            {
                if (AdsEvents.OnBannerAdClickedEvent != null)
                    AdsEvents.OnBannerAdClickedEvent.Invoke();
            }; 

            // Raised when an ad opened full screen content.
            bannerView.OnAdFullScreenContentOpened += () =>
            {
                if (AdsEvents.OnBannerAdOpeningEvent != null)
                    AdsEvents.OnBannerAdOpeningEvent.Invoke();
            };

            // Raised when the ad closed full screen content.
            bannerView.OnAdFullScreenContentClosed += () =>
            {
                if (AdsEvents.OnBannerAdClosedEvent != null)
                    AdsEvents.OnBannerAdClosedEvent.Invoke();
            };
        }


        /// <summary>
        /// Creates the banner view and loads a banner ad.
        /// </summary>
        public void ShowBannerAd(string adKeyword)
        {
            // create our request used to load the ad.
            var _adRequest = new AdRequest.Builder()
                .AddKeyword(adKeyword)
                .Build();

            // send the request to load the ad.
            bannerView.LoadAd(_adRequest);
        }

        /// <summary>
        /// Destroys the ad if it is not null.
        /// </summary>
        public void DestroyAd()
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
                bannerView = null;
            }
        }
    }
}
