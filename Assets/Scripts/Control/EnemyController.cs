using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Combat;
using Bonfire.Attributes;

namespace Bonfire.Control
{

    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject projectileSpawnerPrefab;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private float speedToLook = 0.1f;
        [SerializeField] private float speed = 3.0f;
        [SerializeField] private float distanceToChase = 7.0f;
        [SerializeField] private float distanceToPunch = 1.0f;
        [SerializeField] private float distanceToShoot = 6.0f;
        [SerializeField] private float projectileSpawnerRate = 1.5f;
        [SerializeField] private int rewardPoints = 10; //puntos que gana el jugador al eliminar al enemigo

        [SerializeField] private Animator animatorController;
        
        Health health;

        private bool isMoving = false;
        private bool isPunching = false;
        private bool isFiring = false;
        private float spawningTimeLeft;

        GameObject projectileSpawner = null;

        public enum EnemyType {Fixed, Chaser};
        public EnemyType enemyType;


        void Start()
        {
            spawningTimeLeft = projectileSpawnerRate;
            health = GetComponent<Health>();


            //SetHP();



        }

        /*
        private void SetHP()
        {
            if (enemyType == EnemyType.Fixed)
            {
                hp = 1;
            }
            else
            {
                hp = 2;
            }

        }
        */

        void Update()
        {
            LookAtPlayer(); //mira al jugador ya sea chaser o fixed
            InteractWithCombat();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            if (!IsDead())
            {

                if (isMoving)
                {
                    animatorController.SetBool("isRunning", true);
                }
                else
                {   
                    animatorController.SetBool("isRunning", false);
                }


                if (isPunching)
                {
                    animatorController.SetBool("isPunching", true);
                   // Debug.Log("Punch animation");
                }
                else
                {
                    animatorController.SetBool("isPunching", false);
                }

                isMoving = false;
                isPunching = false;

            }
            else
            {
                //Is Dead Animation
            }

        }



        private void InteractWithCombat()
        {
            switch (enemyType)
            {
                case EnemyType.Fixed:
                    
                    if (MustAttack(distanceToShoot))
                    {
                        RunTimer();
                    }

                    isPunching = false;
                    
                    break;
                
                case EnemyType.Chaser:

                    if (MustChasePlayer())
                    {
                        ChasePlayer();
                        isMoving = true;
                        isPunching = false;
                    }

                    if (MustAttack(distanceToPunch))
                    {
                        //Punch();
                        isMoving = false;
                        isPunching = true;
                    }

                    break;

            }

        }


        private void RunTimer()
        {
            
            //Debug.Log("COMIENZA EL TIMER");

            spawningTimeLeft -= Time.deltaTime;

            if (spawningTimeLeft <= 0)
            {
                ShootProjectile();
                ResetTimer();
            }
            
        }

        private void ResetTimer()
        {
            spawningTimeLeft = projectileSpawnerRate;
        }


        private void ShootProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, rightHand.transform.position, projectilePrefab.transform.rotation);
            projectile.GetComponent<Projectile>().SetProjectileDirection(transform.forward);
            projectile.GetComponent<Projectile>().SetInstigator(gameObject);
        }

        private void Punch()
        {
           // Debug.Log("Punch");
            
        
        }

        private bool MustAttack(float radiusDistance)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return (distanceToPlayer <= radiusDistance);    
        }

        private void ChasePlayer()
        {
            transform.position += (DirectionVectorToPlayer() * speed * Time.deltaTime);
           
        }

        private bool MustChasePlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return (distanceToPlayer <= distanceToChase && distanceToPlayer >= distanceToPunch);
        }

        private void LookAtPlayer()
        {
            Quaternion newRotation = Quaternion.LookRotation(DirectionVectorToPlayer());
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
        }

        private Vector3 DirectionVectorToPlayer()
        {
            return (player.transform.position - transform.position).normalized;

        }


        /* YA FUE IMPLEMENTADO EN LA CLASE "HEALTH"
        public void TakeDamage(float damage)
        {
            hp -= damage;
            if (IsDead())
            {
                //Die
                Destroy(gameObject);
            }

           // Debug.Log(hp);
        }
        */

        public bool IsDead()
        {
            return health.IsDead();
        }

        public int GetRewardPoints()
        {
            return rewardPoints;
        }





    }
}




