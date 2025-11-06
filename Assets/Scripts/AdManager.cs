using UnityEngine;

public class AdManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AdInitializer adsInitializer;
    public InterstialAd interstialAd;
    [SerializeField] bool turnOffInterstialAd = false;
    private bool firstAdShow = false;

    public static AdManager Instance { get; private set; }

    private void Awake()
    {
        if(adsInitializer != null)
        {
            adsInitializer = FindFirstObjectByType<AdInitializer>();
        }
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        adsInitializer.OnAdsInitialized += HandleAdsInitialized;
    }

    private void HandleAdsInitialized()
    {
        if (!turnOffInterstialAd)
        {
            interstialAd.OnInterstitialAdReady += HandleInterstitialReady;
            interstialAd.LoadAd();
        }
    }

    private void HandleInterstitialReady()
    {
        if (!FirstAdShown)
        {
            Debug.Log("Showing first time ad");
            interstitialAd.ShowAd();
            FirstAdShown = true;
        }
        else
        {
            Debug.Log("Next interstitial ad is ready fro manual show!");
        }
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
