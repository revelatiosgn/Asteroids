using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Model;
using System;
using Asteroids.Logic;
using UnityEngine.UI;
using TMPro;

namespace Asteroids.UiController
{
    public class UiStartup : MonoBehaviour
    {
        [SerializeField] private StartUp _startUp;
        [SerializeField] private ShipUi _shipUi;
        [SerializeField] private LaserDelay _laserDelay;
        [SerializeField] private LaserCharges _laserCharges;

        [SerializeField] private Transform _gameplayView;
        [SerializeField] private Transform _gameoverView;

        [SerializeField] private Button _restart;
        [SerializeField] private TMP_Text _score;

        private void Awake()
        {
            _startUp.OnGameStarted += OnGameStarted;

            _restart.onClick.AddListener(() => _startUp.GameplayFSM.ChangeState(GameState.Gameplay));
        }

        private void OnGameStarted()
        {
            _startUp.GameplayFSM.OnState += OnGameState;
        }

        private void OnGameState(GameState gameState)
        {
            if (gameState == GameState.Gameplay)
            {
                _gameplayView.gameObject.SetActive(true);
                _gameoverView.gameObject.SetActive(false);

                _shipUi.Init(_startUp.ObjectLibrary.Ship);
                _laserDelay.Init(_startUp.ObjectLibrary.Ship.LaserGun, _startUp.GameSettings);
                _laserCharges.Init(_startUp.ObjectLibrary.Ship.LaserGun);
            }
            else if (gameState == GameState.Gameover)
            {
                _gameplayView.gameObject.SetActive(false);
                _gameoverView.gameObject.SetActive(true);

                _score.text = $"Score: {_startUp.Scoreboard.Score.ToString()}";
            }
        }
    }
}
