using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using Bonfire.Control;


namespace Bonfire.Combat
{
    public class Snowball : MonoBehaviour
    {

        [SerializeField] private float damage = 1.0f;



        void Start()
        {

        }



        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject target = other.gameObject;
            if (target.tag == "Player")
            {
                target.GetComponent<Health>().TakeDamage(damage);

                AudioSource audioSource = target.GetComponent<AudioSource>();
                audioSource.PlayOneShot(target.GetComponent<PlayerController>().hurtSound);
            }




        }





    }

}
