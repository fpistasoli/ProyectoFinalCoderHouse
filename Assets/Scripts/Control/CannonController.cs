using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float spawningTime = 3.0f;
        [SerializeField] private float bulletDestroyTime = 2.0f;
        private float spawningTimeLeft;

        // Start is called before the first frame update
        void Start()
        {
            ShootBullet();
            ResetTimer();
        }

        // Update is called once per frame
        void Update()
        {
            RunTimer();
        }

        private void RunTimer()
        {
            spawningTimeLeft -= Time.deltaTime;

            if (spawningTimeLeft <= 0)
            {
                ShootBullet();
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            spawningTimeLeft = spawningTime;
        }

        public void ShootBullet()
        {
            SpawnBullet(bulletDestroyTime);
        }

        private void SpawnBullet(float bulletDestroyTime)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            spawnedBullet.GetComponent<BulletController>().SetBulletDestroyTime(bulletDestroyTime);
        }
    }
}

