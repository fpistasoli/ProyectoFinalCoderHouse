using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class Shrinker : MonoBehaviour
    {

        [SerializeField] private float scale = 2.0f;
        private bool hasBeenShrunk = false;


        void Start()
        {

        }


        void Update()
        {
         
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (!hasBeenShrunk)
                {
                    other.transform.localScale /= scale;
                }
                else
                {
                    other.transform.localScale *= scale;
                }
                
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                hasBeenShrunk = !hasBeenShrunk; 
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Collided with " + this.name);
                



            }
            
        }

    }

}