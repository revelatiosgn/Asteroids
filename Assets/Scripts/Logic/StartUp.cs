using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Logic
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        public GameSettings GameSettings => _gameSettings;

        private Updater _updater;
        private ObjectLibrary _objectLibrary;
        private ObjectFactory _objectFactory;
        private EnemySpawner _enemySpawner;
        private PlayerInput _playerInput;
        private PoolManager _poolManager;
        private Scoreboard _scoreboard;

        public ObjectLibrary ObjectLibrary => _objectLibrary;
        public Scoreboard Scoreboard => _scoreboard;

        private GameplayFSM _gameplayFSM;
        public GameplayFSM GameplayFSM => _gameplayFSM;

        public event Action OnGameStarted;

        private void Start()
        {
            StartGame();
        }
        
        private void Update()
        {
            _gameplayFSM.Update(Time.deltaTime);
        }

        private void StartGame()
        {
            _poolManager = new PoolManager();
            _updater = new Updater();
            _objectLibrary = new ObjectLibrary();
            _scoreboard = new Scoreboard();
            _objectFactory = new ObjectFactory(_updater, _gameSettings, _poolManager, _scoreboard);

            _objectLibrary.Ship = _objectFactory.Create<Ship>(ObjectType.Ship);
            _objectLibrary.Ship.OnRelease += () => _objectLibrary.Ship = null;

            _enemySpawner = new EnemySpawner(_objectFactory, _gameSettings, _objectLibrary);
            _updater.Add(_enemySpawner);

            _playerInput = new PlayerInput(_objectLibrary, _gameSettings, _objectFactory);
            _updater.Add(_playerInput);


            _gameplayFSM = new GameplayFSM();
            _gameplayFSM.AddState(GameState.Gameplay, new GameplayState(_updater, _poolManager, _objectFactory, _objectLibrary, _scoreboard));
            _gameplayFSM.AddState(GameState.Gameover, new GameoverState());
            _gameplayFSM.ChangeState(GameState.Gameplay);

            OnGameStarted?.Invoke();
        }
    }
}
