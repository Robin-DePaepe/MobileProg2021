using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsListener
{
    public enum addTypes { reward, interstitial, banner };

    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;

    //ids
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;

    Dictionary<addTypes, string> _AdunitIds = new Dictionary<addTypes, string>() {
        {addTypes.reward, "Rewarded_" },
        {addTypes.interstitial, "Interstitial_" },
        {addTypes.banner, "Banner_" }
    };
    string _androidAdUnitId = "Android";
    string _iOsAdUnitId = "iOS";

    private string _gameId;

    //reward
    [SerializeField] Button _showAdButton;

    //banner
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;



    void Awake()
    {
        InitializeAds();

        //setup the ids and load the ads
        string platform = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOsAdUnitId : _androidAdUnitId;

        for (int i = 0; i < _AdunitIds.Count; i++)
        {
            _AdunitIds[(addTypes)i] += platform;
        }
        //Disable button until ad is ready to show
        _showAdButton.interactable = false;

        //setup banner
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }

    public void InitializeAds()
    {
        Advertisement.AddListener(this);
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        foreach (var id in _AdunitIds)
        {
            LoadAd(id.Value);
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }


    // Load content to the Ad Unit:
    public void LoadAd(string id)
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + id);
        Advertisement.Load(id, this);
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowAd(addTypes type)
    {
        // Disable the button: 
        if (type == addTypes.reward) _showAdButton.interactable = false;

        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _AdunitIds[type]);
        Advertisement.Show(_AdunitIds[type]);
    }

    // Implement Load Listener and Show Listener interface methods:  
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Contains("Rewarded"))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(delegate { ShowAd(_AdunitIds.FirstOrDefault(id => id.Value == adUnitId).Key); });
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { Debug.Log("why u clicking?"); }


    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId.Contains("Rewarded") && showResult.Equals(ShowResult.Finished))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            CoinManager.Instance.AddCurrency(100);
        }
        // Load another ad:
        Advertisement.Load(placementId, this);
    }

    //banner functions

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_AdunitIds[addTypes.banner], options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_AdunitIds[addTypes.banner], options);
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}
