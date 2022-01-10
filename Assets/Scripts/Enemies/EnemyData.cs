using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Enemies
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private float speedToLook;
        [SerializeField] protected float rayToFindPlayerLength;
        [SerializeField] private int rewardPoints;
        [SerializeField] private float projectileSpawnerRate;
        [SerializeField] private float speed;
        [SerializeField] private float distanceToStartPunching;
        [SerializeField] private float punchDamage;

        public float GetSpeedToLook()
        {
            return speedToLook;
        }

        public float GetRayToFindPlayerLength()
        {
            return rayToFindPlayerLength;
        }

        public int GetRewardPoints()
        {
            return rewardPoints;
        }

        public float GetProjectileSpawnerRate()
        {
            return projectileSpawnerRate;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float GetDistanceToStartPunching()
        {
            return distanceToStartPunching;
        }

        public float GetPunchDamage()
        {
            return punchDamage;
        }


    }
}
