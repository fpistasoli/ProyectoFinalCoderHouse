using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Combat;
using Bonfire.Attributes;

namespace Bonfire.Enemies
{

    public class EnemyFixed : Enemy 
    {

        [SerializeField] private GameObject projectileSpawnerPrefab;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject body;

        //[SerializeField] private float projectileSpawnerRate = 1.5f;

        private float spawningTimeLeft;

        private Health healthEnemyFixed;

        //SOUND EFFECTS
        [SerializeField] public AudioClip enemyHurtSound;
        [SerializeField] public AudioClip enemyDeathSound;
        private AudioSource audioSource;


        void Start()
        {
            spawningTimeLeft = enemyData.GetProjectileSpawnerRate();

            healthEnemyFixed = GetComponent<Health>();
            //Debug.Log("HEALTH ENEMY FIXED: " + healthEnemyFixed);

            audioSource = GetComponent<AudioSource>();
        }


        void Update()
        {

        }

        public override void InteractWithCombat()
        {
            RunSpawningProjectileTimer();
        }

        private void RunSpawningProjectileTimer()
        {
            spawningTimeLeft -= Time.deltaTime;

            if (spawningTimeLeft <= 0)
            {
                ShootProjectile();
                ResetTimer();
            }


        }

        private void ResetTimer()
        {
            spawningTimeLeft = enemyData.GetProjectileSpawnerRate();
        }


        private void ShootProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, rightHand.transform.position, projectilePrefab.transform.rotation);
            projectile.GetComponent<Projectile>().SetProjectileDirection(transform.forward);
            projectile.GetComponent<Projectile>().SetInstigator(gameObject);
        }

        //public override void UpdateAnimator()
        //{
        //    base.UpdateAnimator();

            //Add arrow throw animation

        //}


    }
}
