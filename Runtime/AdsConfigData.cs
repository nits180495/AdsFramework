using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    //enum for differnt type of Ads.
    public enum AdsConfigType
    {
        appId,
        bannerAds,
        interstitialAds,
        rewardAds
    }

    //enum for differnt type of Ads Unit ID's.
    public enum AdsType
    {
        ProductionAds,
        TestAds
    }

    [CreateAssetMenu(fileName = "AdsConfigDataSO", menuName = "AdsFramework/AdsConfigData")]
    public class AdsConfigData : ScriptableObject
    {
        private const string ADS_DATA_CONST = "AdsFramework/AdsConfigDataSO";

        //Add device ID's to register those device for showing ads.
        [SerializeField] private List<string> deviceIds = new List<string>() { AdRequest.TestDeviceSimulator };

        //Add Ads ID's to register those device for showing ads.
        [SerializeField] private List<AdUnitId> adUnitIds = new List<AdUnitId>();

        /// <summary>
        /// Return Scriptable Object form Resources Folder.
        /// </summary>
        /// <returns></returns>
        public System.Object GetConfig()
        {
            return Resources.Load(ADS_DATA_CONST);
        }

        #region Ads ID Config
        /// <summary>
        /// Return ID's based on config.
        /// </summary>
        /// <returns></returns>
        public string GetID(AdsConfigType adsConfigType, string unitName)
        {
#if UNITY_ANDROID
                return GetForElement(adsConfigType, unitName).AdsUnitId.AndroidAdUnitId;
#elif UNITY_IOS
                return GetForElement(adsConfigType, unitName).AdsUnitId.IosAdUnitId;
#else
            return null;
#endif
        }

        /// <summary>
        /// Return AdUnityID based on adsConfigType provided.
        /// </summary>
        /// <param name="adsConfigType"></param>
        /// <returns></returns>
        private AdUnitId GetForElement(AdsConfigType adsConfigType, string UnitName)
        {
            AdUnitId _adUnitId = adUnitIds.Find(x => x.AdFormat == adsConfigType && x.AdUnitName == UnitName);

            return _adUnitId;
        }
        #endregion

        #region Test Devcie Config
        /// <summary>
        /// Config all test device ID's.
        /// </summary>
        public void ConfigTestDevices()
        {
#if UNITY_IOS
            MobileAds.SetiOSAppPauseOnBackground(true);
#endif
            RequestConfiguration requestConfiguration = new RequestConfiguration();
            requestConfiguration.TestDeviceIds.AddRange(deviceIds);

            MobileAds.SetRequestConfiguration(requestConfiguration);
        }

        /// <summary>
        /// Add Test Device ID's in List.
        /// </summary>
        /// <param name="ids"></param>
        public void AddDeviceIdsInList(List<string> ids)
        {
            foreach (var id in ids)
                deviceIds.Add(id);
        }
        #endregion
    }

    [Serializable]
    public class AdUnitId
    {
        public string AdUnitName;
        public AdsType Type;
        public AdsConfigType AdFormat;
        public AdUnitType AdsUnitId;
    }

    [Serializable]
    public class AdUnitType
    {
        public string AndroidAdUnitId;
        public string IosAdUnitId;
    }
}