using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bonfire.Control
{
    public class Teleporter : MonoBehaviour
    {

        [SerializeField] private float teleportTime = 2f;
        private float timeRemaining;
        private bool isCollidingWithPlayer = false;


        void Start()
        {
            timeRemaining = teleportTime;
        }


        void Update()
        {

            if (isCollidingWithPlayer)
            {

                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    //Debug.Log(timeRemaining);

                }
                else
                {
                    RandomTeleport();
                }

            }

        }

        private void RandomTeleport()
        {
            Random.InitState((int)DateTime.Now.Ticks);
            float randomXPos = Random.Range(-3.0f, 3.0f);
            float randomZPos = Random.Range(-5.0f, 5.0f);
            float randomYRot = Random.Range(-90.0f, 90.0f);

            transform.position = new Vector3(randomXPos, 2.0f, randomZPos); //dimensiones del floor
            transform.rotation = new Quaternion(0, randomYRot, 0, 1);

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                isCollidingWithPlayer = true;
                Debug.Log("Collided with " + this.name);
            }

        }

        private void OnCollisionExit(Collision collision)
        {

            if (collision.gameObject.tag == "Player")
            {
                isCollidingWithPlayer = false;
                ResetTimer();
                //Debug.Log("Se llamo al on collision exit");

            }


        }

        private void ResetTimer()
        {
            timeRemaining = teleportTime;
        }
    }


}


