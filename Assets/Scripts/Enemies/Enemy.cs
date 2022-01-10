using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using System;

namespace Bonfire.Enemies
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField] protected GameObject player;
        [SerializeField] private Transform rayToFindPlayerOrigin;
        [SerializeField] protected EnemyData enemyData;

        protected bool isMoving = false;

      
        //[SerializeField] protected float rayToFindPlayerLength; //es protected ya que necesita ser accedido y modificado desde las clases hijas (dependiendo si es fixed o chaser la distancia del ray es distinta)
        //[SerializeField] private float speedToLook = 8.0f;
        //[SerializeField] private int rewardPoints = 10; //puntos que gana el jugador al eliminar al enemigo
        //protected Animator enemyAnimator;
        //protected Health health;


        void Start()
        {
            //enemyAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
            //health = GetComponent<Health>();
            // Debug.Log("HEALTH ENEMY: " + health);
            //Debug.Log("HEALTH ENEMY HP: " + health.GetHealthPoints());

        }

       
        void FixedUpdate()
        {
            FindPlayer();
            LookAtPlayer();
        }

        private void Update()
        {
            //UpdateAnimator();
        }

        private void LookAtPlayer()
        {
            Quaternion newRotation = Quaternion.LookRotation(VectorToPlayer().normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, enemyData.GetSpeedToLook() * Time.deltaTime);
        }

        protected Vector3 VectorToPlayer()
        {
            return (player.transform.position - transform.position);
        }


        private void FindPlayer()
        {
            BroadcastRaycast(rayToFindPlayerOrigin.transform);
        }

        private void BroadcastRaycast(Transform rayOrigin)
        {
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, enemyData.GetRayToFindPlayerLength(), 3))
            {
                //Debug.Log("Hit collider: " + hit.collider);

                if(hit.transform.CompareTag("Player"))
                {
                    //Debug.Log("TOCO AL PLAYER");
                    InteractWithCombat();
                }
                
            }
        }

        public virtual void InteractWithCombat()
        {
            //empty
        }

        private void OnDrawGizmos()
        {
            if(!isMoving)
            {
                DrawRay(rayToFindPlayerOrigin.transform);
            }
        }

        private void DrawRay(Transform rayOrigin)
        {
            Gizmos.color = Color.blue;
            Vector3 direction = rayOrigin.TransformDirection(Vector3.forward) * enemyData.GetRayToFindPlayerLength();
            Gizmos.DrawRay(rayOrigin.position, direction);
        }

        //protected bool IsDead()
        //{
       //     return health.IsDead();
       // }

        public int GetRewardPoints()
        {
            return enemyData.GetRewardPoints();
        }

        public EnemyData GetEnemyData()
        {
            return enemyData;
        }



        //public virtual void UpdateAnimator()
        //{
         //   Debug.Log("ANIMATOR ENEMY " + gameObject.name);
        //}


    }

}

