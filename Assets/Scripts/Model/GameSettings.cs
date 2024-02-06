using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Model
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public ShipSettings Ship;
        public AsteroidSettings Asteroid;
        public BulletSettings Bullet;
        public SpawnSettings Spawn;
        public UFOSetting UFO;
        public LaserGunSettings LaserGun;
    }

    [System.Serializable]
    public class ShipSettings
    {
        public InputActionAsset InputActions;
        public float RotationSpeed = 1f;
        [Min(0f)] public float Acceleration = 1f;
        [Min(0f)] public float MaxSpeed = 1f;
        [Min(0f)] public float BulletFireDelay = 1f;
    }

    [System.Serializable]
    public class AsteroidSettings
    {
        [Min(0f)] public float MinSpeed = 1f;
        [Min(0f)] public float MaxSpeed = 10f;
        [Min(1)] public int StartLevel = 2;
        [Min(1)] public int SplitCount = 3;
        public float SplitAcceleration = 1.2f;
    }

    [System.Serializable]
    public class UFOSetting
    {
        [Min(0f)] public float Speed = 3f;
    }

    [System.Serializable]
    public class BulletSettings
    {
        [Min(0f)] public float Speed = 3f;
    }

    [System.Serializable]
    public class SpawnSettings
    {
        public bool SpawnEnemy = true;
        [Min(0f)] public float MinSpawnTime = 1f;
        [Min(0f)] public float MaxSpawnTime = 5f;
        public List<SpawnWeigth> Weights;
    }

    [System.Serializable]
    public class SpawnWeigth
    {
        public ObjectType Type;
        [Min(0f)] public float Weight;
    }

    [System.Serializable]
    public class LaserGunSettings
    {
        [Min(0f)] public float FireDelay = 1f;
        [Min(0f)] public float FireDuration = 0.5f;
        [Min(0f)] public float RechargeDelay = 3f;
        [Min(0)] public int MaxCharges = 3;
    }
}
