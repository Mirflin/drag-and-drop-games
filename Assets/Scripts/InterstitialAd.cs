using System;
using System.Collections;
using UnityEditor.Advertisements;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class InterstialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidUnitId = "Interstitial_Android";
    string _adUnitId;

    public event Action OnInterstitialAdReady;
    public bool isReady = false;
    [SerializeField] Button _interstitialAdButton;

    public void Awake()
    {
        _adUnitId = _androidUnitId;
    }

    private void Update()
    {
        if(AdManager.Instance != null && AdManager.Instance.interstialAd != null) 
        {
            _interstitialAdButton.interactable = isReady;
        }
    }

    public void OnInterstitialAdButtonClicked()
    {
        Debug.Log("Interstitial ad button clicked!");
        ShowInterstitial();
    }

    public void LoadAd()
    {
        if (!Advertisement.isInitialized)
        {
            Debug.LogWarning("Tried to load interstitial ad before unity ads was initialized!");
            return;
        }

        Debug.Log("Loading interstitial ad...");
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (isReady)
        {
            Advertisement.Show(_adUnitId, this);
            isReady = false;
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet!");
        }
    }

    public void ShowInterstitial()
    {
        if(AdManager.Instance.interstialAd != null && isReady)
        {
            Debug.Log("Showing interstitial ad manually!");
            ShowAd();
        }
        else
        {
            Debug.Log("Interstitial ad not ready yet, loading again!");
            LoadAd();
        }
    }

    public void OnUnityAdsLoaded(string placementId)
    {
        Debug.Log("Interstitial ad loaded!");
        _interstitialAdButton.interactable = true;
        isReady = true;
        OnInterstitialAdReady?.Invoke();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Failed to load interstitial ad!");
        LoadAd();
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("User clicked on interstitial ad!");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState, string message)
    {
        if(showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Interstitial ad watched completely!");
            StartCoroutine(SlowDownTimeTemporarily(30f));
            LoadAd();
        }
        else
        {
            Debug.Log("Intestitial ad skipped!");
            LoadAd();
        }
    }
    private IEnumerator SlowDownTimeTemporarily(float sec)
    {
        Time.timeScale = 0.4f;
        Debug.Log("Time slowed down to 0.4x for " + sec + "sec");
        yield return new WaitForSeconds(sec);

        Debug.Log("Time restored to normal!");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Error showing interstitial ad!");
        LoadAd();
    }

    public void OnUnityAsShowStart(string placementId)
    {
        Debug.Log("Showing interstitial ad at this moment!");
        Time.timeScale = 0;
    }

    public void setButton(Button button)
    {
        if(button == null)
        {
            return;
        }
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnInterstitialAdButtonClicked);
        _interstitialAdButton = button;
        _interstitialAdButton.interactable = false;
    }
}
