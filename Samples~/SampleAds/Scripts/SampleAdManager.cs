using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JPackage.AdsFramework;
using UnityEngine.UI;
using GoogleMobileAds.Api;

namespace AdsSample
{
    public class SampleAdManager : MonoBehaviour
    {
        //AdsConfigData an Scriptable obhjec that have configuration data for ads
        [SerializeField] private AdsConfigData adsConfigData;

        private AdsInitializer adsInitializer = new AdsInitializer();

        /// <summary>
        /// Region for Variables realted to Banner Ads.
        /// </summary>
        #region Banner Ads Variables

        [SerializeField] private Button LoadBannerAdsButton;
        [SerializeField] private Button WatchBannerAdsButton;
        [SerializeField] private Button CloseBannerAdsButton;

        private BannerAdsManager bannerAdsManager = new BannerAdsManager();

        #endregion Banner Ads Variables

        /// <summary>
        /// Region for Variables realted to Interstitial Ads.
        /// </summary>
        #region Interstitial Ads Variables

        [SerializeField] private Button LoadInterstitialAdsButton;
        [SerializeField] private Button WatchInterstitialAdsButton;

        private InterstitialAdsManager interstitialAdsManager = new InterstitialAdsManager();

        #endregion Interstitial Ads Variables

        /// <summary>
        /// Region for Variables realted to Reward Ads.
        /// </summary>
        #region Reward Ads Variables

        [SerializeField] private Button LoadRewardAdsButton;
        [SerializeField] private Button WatchRewardAdsButton;

        private RewardAdsManager rewardAdsManager = new RewardAdsManager();

        #endregion Reward Ads Variables

        private void Awake()
        {
            adsConfigData = (AdsConfigData)adsConfigData.GetConfig();//Getting Refrence of Ads Config
            adsConfigData.ConfigTestDevices();//test device ids config. Add test id's in scriptable object.
            adsInitializer.AdsInit();//ads initialization
        }

        private void OnEnable()
        {
            //Adding listners to all buttons
            LoadBannerAdsButton.onClick.AddListener(LoadBannerAds);
            WatchBannerAdsButton.onClick.AddListener(WatchBannerAds);
            CloseBannerAdsButton.onClick.AddListener(CloseBannerAds);
            LoadInterstitialAdsButton.onClick.AddListener(LoadInterstitialAds);
            WatchInterstitialAdsButton.onClick.AddListener(WatchInterstitialAds);
            LoadRewardAdsButton.onClick.AddListener(LoadRewardAds);
            WatchRewardAdsButton.onClick.AddListener(WatchRewardAds);

            //Adding callback for the listners in Ads
            AdsEvents.OnInterstitialAdClosedEvent += CloseInterstitialAds;
            AdsEvents.OnRewardAdClosedEvent += CloseRewardAds;
        }

        private void OnDisable()
        {
            //Removing listners from all buttons
            LoadBannerAdsButton.onClick.RemoveAllListeners();
            WatchBannerAdsButton.onClick.RemoveAllListeners();
            CloseBannerAdsButton.onClick.AddListener(CloseBannerAds);
            LoadInterstitialAdsButton.onClick.RemoveAllListeners();
            WatchInterstitialAdsButton.onClick.RemoveAllListeners();
            LoadRewardAdsButton.onClick.RemoveAllListeners();
            WatchRewardAdsButton.onClick.RemoveAllListeners();

            //Removing callback for the listners in Ads
            AdsEvents.OnInterstitialAdClosedEvent -= CloseInterstitialAds;
            AdsEvents.OnRewardAdClosedEvent -= CloseRewardAds;
        }

        /// <summary>
        /// Banner Ads Code.
        /// </summary>
        #region Banner Ads

        /// <summary>
        /// Load Banner Ad and set UI.
        /// </summary>
        private void LoadBannerAds()
        {
            LoadBannerAdsButton.interactable = false;
            WatchBannerAdsButton.interactable = true;
            bannerAdsManager.CreateBannerView(adsConfigData.
                GetID(AdsConfigType.bannerAds, "BannerAds"), AdSize.Type.Standard, AdPosition.Top);
        }

        /// <summary>
        /// Show Banner Ad on screen.
        /// </summary>
        private void WatchBannerAds()
        {
            WatchBannerAdsButton.interactable = false;
            CloseBannerAdsButton.interactable = true;
            bannerAdsManager.ShowBannerAd("unity-admob-sample");
        }

        /// <summary>
        /// Close Banner Ad and destroy ad.
        /// </summary>
        private void CloseBannerAds()
        {
            LoadBannerAdsButton.interactable = true;
            CloseBannerAdsButton.interactable = false;
            bannerAdsManager.DestroyAd();
        }

        #endregion Banner Ads

        /// <summary>
        /// Interstitial Ads Code.
        /// </summary>
        #region Interstitial Ads

        /// <summary>
        /// Load Interstitial Ads and set UI.
        /// </summary>
        private void LoadInterstitialAds()
        {
            LoadInterstitialAdsButton.interactable = false;
            WatchInterstitialAdsButton.interactable = true;
            interstitialAdsManager.LoadInterstitialAd(adsConfigData.
                GetID(AdsConfigType.interstitialAds, "InterstitialAds"));
        }

        /// <summary>
        /// Show Interstitial ad on screen.
        /// </summary>
        private void WatchInterstitialAds()
        {
            WatchInterstitialAdsButton.interactable = false;
            interstitialAdsManager.ShowInterstitialAd();
        }

        /// <summary>
        /// Close Interstitial Ad and destroy ad.
        /// </summary>
        private void CloseInterstitialAds()
        {
            LoadInterstitialAdsButton.interactable = true;
            interstitialAdsManager.DestroyAd();//destroying ad when it is closed.
        }

        #endregion Interstitial Ads

        /// <summary>
        /// Reward Ads Code.
        /// </summary>
        #region Reward Ads

        /// <summary>
        /// Load Reward Ads and set UI.
        /// </summary>
        private void LoadRewardAds()
        {
            LoadRewardAdsButton.interactable = false;
            WatchRewardAdsButton.interactable = true;
            rewardAdsManager.LoadRewardedAd(adsConfigData.
                GetID(AdsConfigType.rewardAds, "RewardAds"));
        }

        /// <summary>
        /// Show Reward ad on screen.
        /// </summary>
        private void WatchRewardAds()
        {
            WatchRewardAdsButton.interactable = false;
            rewardAdsManager.ShowRewardedAd();
        }

        /// <summary>
        /// Close Interstitial Ad and destroy ad.
        /// </summary>
        private void CloseRewardAds()
        {
            LoadRewardAdsButton.interactable = true;
            rewardAdsManager.DestroyAd();//destroying ad when it is closed.
        }

        #endregion Reward Ads
    }
}