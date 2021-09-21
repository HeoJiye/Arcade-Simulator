using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdManager : MonoBehaviour
{
    public ArcadeManager arcade;

    private RewardedAd rewardedAd;

    void Start()
    {
        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("RewardedAdFailed! Please check your network.");
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print("RewardedAdFailed! Please check your network.");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("You got 5 coins!");

        arcade.gotCoin();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();
    }

    //

    public bool IsLoaded() {
        return this.rewardedAd.IsLoaded();
    }

    public void Show() {
        this.rewardedAd.Show();
    }
}
