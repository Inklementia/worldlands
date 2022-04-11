using System;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Sources.Scripts.UI
{
    public class AdsContinueGame : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
        string _adUnitId;

        private IGameFactory _gameFactory;

        [SerializeField] private GameOver gameOver;
        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            
              // Get the Ad Unit ID for the current platform:
                        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                            ? _iOsAdUnitId
                            : _androidAdUnitId;
        }

        private void Start()
        {
            LoadAd();
        }

        public void RevivePlayer()
        {
            GameObject attackButton = GameObject.FindWithTag("AttackButton");
            GameObject pickupButton = GameObject.FindWithTag("PickupButton");
            PlayerUI playerUI = GameObject.FindWithTag("PlayerUI").GetComponent<PlayerUI>();
            //load player
            GameObject player = _gameFactory.CreatePlayer(gameOver.PlayerDeathPoint.gameObject);
            player.GetComponent<PlayerInputHandler>()._attackButtonGo = attackButton;
            player.GetComponent<PlayerInputHandler>()._pickUpButtonGo = pickupButton;
            player.GetComponent<PlayerEntity>().playerUI = playerUI;
        }
        
        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
 
        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }
 
        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }
 
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }
 
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId == _adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                Debug.Log("VIDEO FINISHED");
                RevivePlayer();
                //pauseButtonCanvas.enabled = true;
            }
            else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
            {
                // do not reward the user for skipping the ad
            }
            else if (showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
            {
                Debug.Log("Unknown");
            }
            
        }
    }
    
}