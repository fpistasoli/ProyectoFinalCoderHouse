using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class PlayerController : MonoBehaviour
    {

        public float speed = 3.0f;
        public float rotationSpeed = 150.0f;
        private bool isMoving = false;
        private bool isTurning = false;
        private float translation; 
        private float rotation;
        private Vector3 translationVelocity;
        private Vector3 rotationVelocity;

        //Health health;

        private void Awake()
        {
            //health = GetComponent<Health>();
        }

        void Start()
        {

        }

        void Update()
        {
            InteractWithMovement();
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

        }

        private void UpdateAnimator()
        {
            Animator animatorController = GetComponent<Animator>();

            if (isMoving) // y/o girar
            {
                animatorController.SetFloat("Forward", 1.0f);
                animatorController.SetFloat("Turn", 0.0f);
            } else { // no se esta moviendo
                if (isTurning)
                {
                    animatorController.SetFloat("Forward", 0.0f);
                    animatorController.SetFloat("Turn", 1.0f);
                } else { // idle
                    animatorController.SetFloat("Forward", 0.0f);
                    animatorController.SetFloat("Turn", 0.0f);
                }
            }

            isMoving = false;
            isTurning = false;

        }
    }

}