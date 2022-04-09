using System;
using _Sources.Scripts.Infrastructure.Factory;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class ContinueGame : MonoBehaviour
    {
        private IGameFactory _gameFactory;

        [SerializeField] private GameOver gameOver;
        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
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
    }
}