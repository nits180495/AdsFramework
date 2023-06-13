using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace JPackage.AdsFramework
{
    public class AdsInitializer
    {
        /// <summary>
        /// Initialise Mobile Ads.
        /// </summary>
        public void AdsInit()
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                if (AdsEvents.AdsInitComplete != null)
                    AdsEvents.AdsInitComplete.Invoke();
            });
        }
    }
}