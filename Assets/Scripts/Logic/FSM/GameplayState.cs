using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Model;
using UnityEngine;

namespace Asteroids.Logic
{
    public class GameplayState : BaseFSMState<GameState>
    {
        private Updater _updater;
        private PoolManager _poolManager;
        private ObjectFactory _objectFactory;
        private ObjectLibrary _objectLibrary;
        private Scoreboard _scoreboard;

        public GameplayState(Updater updater, PoolManager poolManager, ObjectFactory objectFactory, ObjectLibrary objectLibrary, Scoreboard scoreboard)
        {
            _updater = updater;
            _poolManager = poolManager;
            _objectFactory = objectFactory;
            _objectLibrary = objectLibrary;
            _scoreboard = scoreboard;
        }

        public override void OnEnter()
        {
            _poolManager.Dispose();
            _scoreboard.ResetScore();

            var ship = _objectFactory.Create<Ship>(ObjectType.Ship);
            _objectLibrary.Ship = ship;

            ship.OnRelease += () => _objectLibrary.Ship = null;
            ship.OnRelease += () => _fsm.ChangeState(GameState.Gameover);
        }

        public override void OnUpdate(float dt) 
        {
            _updater.Update(dt);
        }
    }
}