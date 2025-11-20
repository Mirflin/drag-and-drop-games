using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerAd : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    string _adUnitId;

    public bool isBannerVisible = false;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Awake()
    {
        _adUnitId = _androidAdUnitId;
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void LoadBanner()
    {
        if(!Advertisement.isInitialized)
        {
            Debug.Log("Tried to load banner ad before Unity ads was initialized!");
            return;
        }

        Debug.Log("Loading Banner ad!");
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner ad loaded!");
        ShowBannerAd();
    }

    void OnBannerError(string message)
    {
        Debug.LogWarning("Banner Error: "+message);
        LoadBanner();
    }

    public void ShowBannerAd()
    {

            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            Advertisement.Banner.Show(_adUnitId, options);
        
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked()
    {
        Debug.Log("User clicked on banner ad!");
    }

    void OnBannerHidden()
    {
        Debug.Log("Banner is hidden!");
        isBannerVisible = false;
    }

    void OnBannerShown()
    {
        Debug.Log("Banner ad is visible!");
        isBannerVisible = true;
    }
}
