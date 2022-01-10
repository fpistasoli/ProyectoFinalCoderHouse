using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Control;

namespace Bonfire.Combat
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public GameObject projectilePrefab;
        [SerializeField] private float spawningTime = 3.0f;
        [SerializeField] private float projectileDestroyTime = 2.0f;
        //[SerializeField] private Projectile projectile = null; //por defecto es un arma SIN proyectil (flecha y arco vs unequipped o vs sword)

        private float spawningTimeLeft;

        // Start is called before the first frame update
        void Start()
        {
            ShootProjectile();
            ResetTimer();

        }

        // Update is called once per frame
        void Update()
        {
            RunTimer();
        }

        /*public bool HasProjectile()
        {
            return projectile != null;
        }*/

       

        private void RunTimer()
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
            spawningTimeLeft = spawningTime;
        }

        public void ShootProjectile()
        {
            SpawnProjectile(projectileDestroyTime);
        }

        private void SpawnProjectile(float projectileDestroyTime)
        {
            GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            spawnedProjectile.GetComponent<Projectile>().SetProjectileDestroyTime(projectileDestroyTime);
        }

        public void SetProjectileDirection(Vector3 direction)
        {
            projectilePrefab.GetComponent<Projectile>().SetProjectileDirection(direction);
        }


    }
}

