using System.Collections;
using System.Collections.Generic;
using CAS;
using UnityEngine;
//using Firebase.Analytics;
using TMPro;
using System;

public class CASAds : MonoBehaviour
{
    public static CASAds instance = null;
    public static IMediationManager _manager = null;
    private static IAdView _lastAdView = null;
    private static IAdView _lastMrecAdView = null;
    private static Action _lastAction = null;
    public static bool CanAppOpenShow = true;
    private static bool m_IsStartupAppOpenAdShown;
    internal float AppOpenTimeToDelay = 2;
    public GameObject NoInternet;
    private bool isInterstitialShowing = false;
    private bool isRewardedShowing = false;
    //private Action lastAction;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        CAS.MobileAds.settings.isExecuteEventsOnUnityThread = true;
        Init();
        CheckForStartupAd();
    }

    private void Update()
    {
        NoInternet.SetActive(Application.internetReachability == NetworkReachability.NotReachable);
    }

    private void _manager_OnInterstitialAdImpression(AdMetaData meta)
    {
        CanAppOpenShow = false;

        //var impressionParameters = new[] {
        //    new Parameter("ad_platform", "CAS"),
        //    new Parameter("ad_source", meta.network.ToString()),
        //    new Parameter("ad_unit_name", meta.identifier),
        //    new Parameter("ad_format", meta.type.ToString()),
        //    new Parameter("value", meta.revenue),
        //    new Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
        //};
        //FirebaseAnalytics.LogEvent("ad_impression_CAS", impressionParameters);
    }

    private void Init()
    {
        _manager = MobileAds.BuildManager()
            .WithInitListener(CreateAdView)
            .Initialize();

        GameAnalyticsSDK.GameAnalytics.Initialize();
        InterstitialEvents();
        RewardedEvents();
        BannerEvents();
        AppOpenEvents();
        LoadAppOpen();
    }

    void AppOpenEvents()
    {
        _manager.OnAppOpenAdLoaded += _manager_OnAppOpenAdLoaded;
        _manager.OnAppOpenAdClosed += _manager_OnAppOpenAdClosed;
        _manager.OnAppOpenAdFailedToLoad += _manager_OnAppOpenAdFailedToLoad;
    }

    private void _manager_OnAppOpenAdFailedToLoad(AdError error)
    {
        Invoke(nameof(LoadAppOpen), AppOpenTimeToDelay);
    }

    private void _manager_OnAppOpenAdLoaded()
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAdLoaded);
    }

    private void _manager_OnAppOpenAdClosed()
    {
        Invoke(nameof(LoadAppOpen), AppOpenTimeToDelay);
    }

    void AppOpenCheck()
    {
        CanAppOpenShow = true;
    }

    public void LoadAppOpen()
    {
        _manager.LoadAd(AdType.AppOpen);
    }

    public void ShowAppOpen()
    {
        if (_manager.IsReadyAd(AdType.AppOpen) && CanAppOpenShow && !isInterstitialShowing && !isRewardedShowing)
        {
            Debug.Log("Showing App Open");
            _manager.ShowAd(AdType.AppOpen);
        }
        else
        {
            LoadAppOpen();
        }
    }

    private void BannerEvents() { }

    #region Rewarded Events
    private void RewardedEvents()
    {
        _manager.OnRewardedAdCompleted += _manager_OnRewardedAdCompleted;
        _manager.OnRewardedAdImpression += _manager_OnInterstitialAdImpression;
        _manager.OnRewardedAdFailedToLoad += _manager_OnRewardedAdFailedToLoad;
        _manager.OnRewardedAdClicked += _manager_OnRewardedAdClicked;
        _manager.OnRewardedAdLoaded += _manager_OnRewardedAdLoaded;
        _manager.OnRewardedAdShown += _manager_OnRewardedAdShown;
        _manager.OnRewardedAdClosed += _manager_OnRewardedAdClosed;
    }

    private void _manager_OnRewardedAdClosed()
    {
        isRewardedShowing = false;
        Invoke(nameof(AppOpenCheck), AppOpenTimeToDelay);
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdClosed);
    }

    private void _manager_OnRewardedAdShown()
    {
        isRewardedShowing = true;
        CanAppOpenShow = false;
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdDisplayed);
    }

    private void _manager_OnRewardedAdLoaded()
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdLoaded);
    }

    private void _manager_OnRewardedAdClicked()
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdClicked);
    }

    private void _manager_OnRewardedAdCompleted()
    {
        _lastAction.Invoke();
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdClosed);
    }

    private void _manager_OnRewardedAdFailedToLoad(AdError error)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdNoInventory);
    }
    #endregion

    #region Interstitial Events
    private void InterstitialEvents()
    {
        _manager.OnInterstitialAdImpression += _manager_OnInterstitialAdImpression;
        _manager.OnInterstitialAdFailedToLoad += _manager_OnInterstitialAdFailedToLoad;
        _manager.OnInterstitialAdFailedToShow += _manager_OnInterstitialAdFailedToShow;
        _manager.OnInterstitialAdLoaded += _manager_OnInterstitialAdLoaded;
        _manager.OnInterstitialAdShown += _manager_OnInterstitialAdShown;
        _manager.OnInterstitialAdClosed += _manager_OnInterstitialAdClosed;
    }

    private void _manager_OnInterstitialAdClosed()
    {
        isInterstitialShowing = false;
        Invoke(nameof(AppOpenCheck), AppOpenTimeToDelay);
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdClosed);
    }

    private void _manager_OnInterstitialAdShown()
    {
        isInterstitialShowing = true;
        CanAppOpenShow = false;
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdDisplayed);
    }

    private void _manager_OnInterstitialAdLoaded()
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdLoaded);
    }

    private void _manager_OnInterstitialAdFailedToShow(string error)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdFailToShow);
    }

    private void _manager_OnInterstitialAdFailedToLoad(AdError error)
    {
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdFailToLoad);
    }
    #endregion

    private void CreateAdView(bool success, string error)
    {
        if (PlayerPrefs.GetInt("NoAds") < 1)
        {
            _lastAdView = _manager.GetAdView(AdSize.AdaptiveFullWidth);
            _lastMrecAdView = _manager.GetAdView(AdSize.MediumRectangle);
            _lastAdView.SetActive(false);
            _lastMrecAdView.SetActive(false);
        }
    }

    public void ShowBanner(AdPosition position)
    {
        if (PlayerPrefs.GetInt("NoAds") < 1)
        {
            if (_lastAdView == null)
            {
                CreateAdView(true, "");
            }

            if (_lastAdView != null)
            {
                _lastAdView.position = position;
                _lastAdView.SetActive(true);
            }
        }
    }

    public void ShowMrecBanner(AdPosition position)
    {
        if (PlayerPrefs.GetInt("NoAds") < 1)
        {
            if (_lastMrecAdView == null)
            {
                CreateAdView(true, "");
            }

            if (_lastMrecAdView != null)
            {
                _lastMrecAdView.position = position;
                _lastMrecAdView.SetActive(true);
            }
        }
    }

    public void HideBanner()
    {
        if (_lastAdView != null)
        {
            _lastAdView.SetActive(false);
        }
    }

    public void HideMrecBanner()
    {
        if (_lastMrecAdView != null)
        {
            _lastMrecAdView.SetActive(false);
        }
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("NoAds") < 1)
        {
            _manager?.ShowAd(AdType.Interstitial);
        }
    }

    public void ShowRewarded(Action complete)
    {
        if (_manager == null)
            return;

        if (_lastAction != null)
        {
            _manager.OnRewardedAdCompleted -= _lastAction;
        }

        _lastAction = complete;
        _manager.OnRewardedAdCompleted += _lastAction;
        _manager?.ShowAd(AdType.Rewarded);
    }

    public void CheckForStartupAd()
    {
        if (!m_IsStartupAppOpenAdShown)
        {
            ShowAppOpen();
            m_IsStartupAppOpenAdShown = true;
        }
    }
}
