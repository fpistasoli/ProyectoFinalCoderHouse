using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage = 3.0f;
        private Vector3 direction;
        private float startTime;
        private float bulletDestroyTime;

        // Start is called before the first frame update
        void Start()
        {
            startTime = Time.time;
            SetBulletDestroyTime(2.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time - startTime < bulletDestroyTime) //the bullet is still alive
            {
                //Debug.Log(cannonGameObject.GetComponent<CannonController>().bulletDestroyTime);
                MoveEnemy();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    transform.localScale *= 2.0f;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetBulletDestroyTime(float destroyTime)
        {
            bulletDestroyTime = destroyTime;
        }

        public void SetBulletDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        private void MoveEnemy()
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }

}