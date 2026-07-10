using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
public class AdBannerController : MonoBehaviour
{
    private Banner banner;
   
    public void DisplayAd()
    {
        RequestBanner();
        if (banner != null)
        {
            banner.Show();
        }
    }
    public void DestroyAd()
    {
        if (banner != null)
        {
            banner.Destroy();
        }
    }
    private void RequestBanner()
    {

        //Sets COPPA restriction for user age under 13
        //MobileAds.SetAgeRestrictedUser(true);

        // Replace demo Unit ID 'demo-banner-yandex' with actual Ad Unit ID
        string adUnitId = "demo-banner-yandex";
        AdRequest adRequest = new(adUnitId);
        if (this.banner != null)
        {
            this.banner.Destroy();
        }
        // Set sticky banner width
        BannerAdSize bannerSize = BannerAdSize.Inline(GetScreenWidthDp(), 50);

        
        this.banner = new Banner(bannerSize, AdPosition.TopCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;/* 
        this.banner.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.banner.OnLeftApplication += this.HandleLeftApplication; */
        this.banner.OnAdClicked += this.HandleAdClicked;
        this.banner.OnImpression += this.HandleImpression;
        banner.LoadAd(adRequest);
        Debug.Log("Banner is requested");
    }

    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }

    
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        //this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        Debug.Log("HandleAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleLeftApplication event received");
    }

    /* public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleReturnedToApplication event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeftApplication event received");
    } */

    public void HandleAdClicked(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClicked event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        Debug.Log("HandleImpression event received with data: " + data);
    }
}
