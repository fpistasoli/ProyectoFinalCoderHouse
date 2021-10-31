using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{

    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private GameObject player; 
        [SerializeField] private float speedToLook = 0.1f;
        [SerializeField] private float speed = 3.0f;
        public enum EnemyType {Fixed, Chaser};
        public EnemyType enemyType; 


        void Start()
        {
            
        }

       
        void Update()
        {
            LookAtPlayer(); //mira al jugador ya sea chaser o fixed

            if(enemyType == EnemyType.Chaser && MustChasePlayer())
            {
                ChasePlayer();
            }
            
        }

        private void ChasePlayer()
        {
            transform.position += (DirectionVectorToPlayer() * speed * Time.deltaTime);
           
        }

        private bool MustChasePlayer()
        {
            return (Vector3.Distance(player.transform.position, transform.position) >= 2.0f);
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

       




    }



}


