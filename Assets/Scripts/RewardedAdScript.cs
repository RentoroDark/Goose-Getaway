using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class RewardedAdScript : MonoBehaviour
{
    private String message = "";

    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;
    public event Action rewardShown;


    public void Awake()
    {
        this.rewardedAdLoader = new RewardedAdLoader();
        RequestRewardedAd();
    }
    public void ShowAd()
    {
        ShowRewardedAd();
    }

    private void RequestRewardedAd()
    {
        this.DisplayMessage("RewardedAd is not ready yet");

        if (this.rewardedAd != null)
        { 
            this.rewardedAd.Destroy();
        }

        // Replace demo Unit ID 'demo-rewarded-yandex' with actual Ad Unit ID
        string adUnitId = "demo-rewarded-yandex";

        AdRequest adRequest = new(adUnitId);

        this.rewardedAdLoader.LoadAd(adRequest, HandleAdLoaded, HandleAdFailedToLoad);
        this.DisplayMessage("Rewarded Ad is requested");
        
    }

    private void ShowRewardedAd()
    {
        if (this.rewardedAd == null)
        {
            this.DisplayMessage("RewardedAd is not ready yet");
            return;
        }

        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        this.rewardedAd.OnAdShown += this.HandleAdShown;
        this.rewardedAd.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.rewardedAd.OnAdImpression += this.HandleImpression;
        this.rewardedAd.OnAdDismissed += this.HandleAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleRewarded;

        this.rewardedAd.Show();
    }

    
    private void DisplayMessage(String message)
    {
        this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
        MonoBehaviour.print(message);
    }


    public void HandleAdLoaded(RewardedAd ad)
    {
        this.DisplayMessage("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(AdFailedToLoadEventArgs arg)
    {
        this.DisplayMessage(
            $"HandleAdFailedToLoad event received with message: {arg.Message}");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdShown event received");
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdDismissed event received");

        this.rewardedAd.Destroy();
        this.rewardedAd = null;
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage($"HandleImpression event received with data: {data}");
    }

    public void HandleRewarded(object sender, Reward args)
    {
        this.DisplayMessage($"HandleRewarded event received: amout = {args.amount}, type = {args.type}");
        rewardShown?.Invoke();
        RequestRewardedAd();
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            $"HandleAdFailedToShow event received with message: {args.Message}");
    }

}

