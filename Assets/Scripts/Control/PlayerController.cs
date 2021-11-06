using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed = 3.0f; //SerializeField permite cambiar una variable privada desde el inspector, pero no permite modificar desde otras clases
        [SerializeField] private float rotationSpeed = 150.0f;

        private bool isMoving = false;  
        private bool isTurning = false;
        private float translation; 
        private float rotation;
        private Vector3 translationVelocity;
        private Vector3 rotationVelocity;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject body;

        [SerializeField] private Animator animatorController;

        //AudioSource footstepsSound;

        //Health health;

        private void Awake()
        {
            //health = GetComponent<Health>();
        }

        void Start()
        {
            //footstepsSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
            UpdateAnimator();
       
        }

        private void InteractWithMovement()
        {
            // Get the horizontal and vertical axis.
            // By default they are mapped to the arrow keys.
            // The value is in the range -1 to 1
            translation = Input.GetAxis("Vertical") * speed;

            rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            if (translation != 0)
            {
                isMoving = true;
            }

            if (rotation != 0)
            {
                isTurning = true;
            }

            // Make it move 10 meters per second instead of 10 meters per frame...
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            // Move translation along the object's z-axis
            translationVelocity = new Vector3(0, 0, translation);
            transform.Translate(translationVelocity);

            // Rotate around our y-axis
            rotationVelocity = new Vector3(0, rotation, 0);
            transform.Rotate(rotationVelocity);

            //if (isMoving) { PlayFootstepsSound(); }

        }

        private void InteractWithCombat()
        {
           
            if (Input.GetButtonDown("Fire1"))
            {
                
                GameObject bullet = Instantiate(bulletPrefab, rightHand.transform.position, bulletPrefab.transform.rotation);


                //Quaternion.Euler(-90, transform.rotation.y, 0)


                //bullet.transform.LookAt(transform.position);


                bullet.GetComponent<BulletController>().SetBulletDirection(transform.forward);    
            }
                
        }

        private Vector3 GetTranslationDirection()
        {
            return Vector3.Normalize(translationVelocity); //obtengo la direccion de la traslacion al normalizarlo (vector unitario)
        }

        private void UpdateAnimator()
        {

            if (isMoving) // y/o girar
            {
                //animatorController.SetFloat("Forward", 1.0f);
                //animatorController.SetFloat("Turn", 0.0f);

                animatorController.SetBool("isRunning", true);

            } else { // no se esta moviendo

                animatorController.SetBool("isRunning", false);

                if (isTurning)
                {
                    //animatorController.SetFloat("Turn", 1.0f);
                } else { // idle
                    //animatorController.SetFloat("Turn", 0.0f);
                }
            }

            isMoving = false;
            isTurning = false;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Floor")
            {
                Debug.Log("Collided with " + collision.gameObject.name);
            }

            if (collision.gameObject.GetComponent<Shrinker>() != null)
            {
                Debug.Log(collision.gameObject.name + " has Shrinker component");
            }



        }




        /*
        private void PlayFootstepsSound()
        {
            footstepsSound.Play();

        }
        */

    }

}