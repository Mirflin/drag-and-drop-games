using System;
using UnityEditor.Advertisements;
using UnityEngine;

public class AdInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    public event Action OnAdsInitialized;

    public void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _gameId = _androidGameId;
#endif
        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Init complete");
        OnAdsInitialized?.Invoke();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity ads initialization failed: { error.ToSring()} - { message }");
    }
    public void UnityAdsInitializationError(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity ads initialization failed: {error.ToSring()} - {message}");
    }
}
