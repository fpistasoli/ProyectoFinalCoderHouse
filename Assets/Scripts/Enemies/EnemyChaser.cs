using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;

namespace Bonfire.Enemies
{
    public class EnemyChaser : Enemy
    {

        private bool isPunching = false;
        private Rigidbody enemyRigidBody;
        private Health healthEnemyChaser;
        private Animator animatorEnemyChaser;

        //[SerializeField] private float speed = 3.0f;
        //[SerializeField] private float distanceToStartPunching = 1.0f;

        //SOUND EFFECTS
        [SerializeField] public AudioClip enemyHurtSound;
        [SerializeField] public AudioClip enemyDeathSound;
        private AudioSource audioSource;

        void Start()
        {
            enemyRigidBody = GetComponent<Rigidbody>();
            healthEnemyChaser = GetComponent<Health>();
            animatorEnemyChaser = gameObject.transform.GetChild(0).GetComponent<Animator>();

            audioSource = GetComponent<AudioSource>();

            //Debug.Log("HEALTH ENEMY CHASER: " + healthEnemyChaser);
        }


        void Update()
        {
            UpdateAnimator();
            Debug.Log("CHASER IS MOVING? " + isMoving);
        }

        public override void InteractWithCombat()
        {

            if (MustChase())
            {
                ChasePlayer();
                isMoving = true;
                isPunching = false;
            } 

            else

            {
                if (MustPunch())
                {
                    //Debug.Log("DEBE ATACAR");
                    isMoving = false;
                    isPunching = true;

                }

                else

                {
                    isMoving = false;
                    isPunching = false;
                }

            }
         

        }

        private bool MustChase()
        {
            float distanceToPlayer = Vector3.Magnitude(VectorToPlayer());
            return (distanceToPlayer <= enemyData.GetRayToFindPlayerLength() && distanceToPlayer >= enemyData.GetDistanceToStartPunching());
        }

        private void ChasePlayer()
        {
            transform.position += (VectorToPlayer().normalized * enemyData.GetSpeed() * Time.deltaTime);
            //enemyRigidBody.AddForce(DirectionVectorToPlayer() * speed, ForceMode.Impulse);
        }

        private bool MustPunch()
        {
            float distanceToPlayer = Vector3.Magnitude(VectorToPlayer());
            return (distanceToPlayer <= enemyData.GetDistanceToStartPunching());
        }

        
        private void UpdateAnimator()
        {
            //base.UpdateAnimator();
            //Debug.Log("ENEMY IS DEAD?: " + healthEnemyChaser.IsDead());
            if (!healthEnemyChaser.IsDead())
            {
               
                if (isMoving)
                {
                    //Debug.Log("ENEMY IS MOVING");
                    animatorEnemyChaser.SetBool("isRunning", true);
                }
                else
                {
                    animatorEnemyChaser.SetBool("isRunning", false);
                }


                if (isPunching)
                {
                    //Debug.Log("ENEMY IS PUNCHING");
                    animatorEnemyChaser.SetBool("isPunching", true);
                    // Debug.Log("Punch animation");

                }
                else
                {
                    animatorEnemyChaser.SetBool("isPunching", false);
                }

            }
            else
            {
                //Is Dead Animation
            }
        }
        

    }

 
}
