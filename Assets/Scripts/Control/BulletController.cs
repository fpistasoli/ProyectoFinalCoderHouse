using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class BulletController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float damage = 3.0f;
        public Vector3 direction = Vector3.right;
        private float startTime;
        //public GameObject cannonGameObject;
        private float bulletDestroyTime;

        // Start is called before the first frame update
        void Start()
        {
            startTime = Time.time;
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

        private void MoveEnemy()
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }

}