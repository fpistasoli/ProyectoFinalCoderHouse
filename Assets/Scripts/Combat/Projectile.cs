using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using Bonfire.Control;
using Bonfire.Enemies;


namespace Bonfire.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Vector3 arrowRotationAngle;
        [SerializeField] private float speed;
        [SerializeField] private float damage = 0;
        //[SerializeField] private Transform target = null;

        private Vector3 direction;
        private float startTime;
        private float projectileDestroyTime;

        private GameObject instigator = null; //objeto que lanza el proyectil
        private Rigidbody rb;

        

        // Start is called before the first frame update
        void Start()
        {
            startTime = Time.time;
            SetProjectileDestroyTime(2.0f);
            SetDamagePoints();

            rb = GetComponent<Rigidbody>();


            //Debug.Log(direction);
        }

        private void SetDamagePoints()
        {
            if (instigator.tag == "Player")
            {
                damage = ProjectileConfig.sharedInstance.GetDamage();
                Debug.Log("DAMAGE POINTS OF PLAYER'S PROJECTILE: " + damage);
            }
            else if (instigator.tag == "Enemy")
            {
                damage = 3.0f;
                Debug.Log("DAMAGE POINTS OF ENEMY'S PROJECTILE: " + damage);
            }

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(Time.time - startTime < projectileDestroyTime) //the bullet is still alive
            {
                //Debug.Log(cannonGameObject.GetComponent<CannonController>().bulletDestroyTime);
                MoveEnemy();
                    
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetProjectileDestroyTime(float destroyTime)
        {
            projectileDestroyTime = destroyTime;
        }

        public void SetProjectileDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        private void MoveEnemy()
        {
            //rb.AddForce(speed * direction.normalized, ForceMode.Impulse);

            transform.Translate(speed * Time.deltaTime * direction, Space.World);

            // transform.Rotate(0, arrowRotationAngle, 0, Space.Self); 

            //arrowRotationAngle += 1.0f;

            //Debug.Log(direction);


            transform.Rotate(arrowRotationAngle * Time.deltaTime * 100);







        }

        public float GetDamage()
        {
            return damage;
        }

        public void SetInstigator(GameObject instigator)
        {
            this.instigator = instigator; 
        }

        public GameObject GetInstigator()
        {
            return instigator;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject target = other.gameObject;
            if ((target.tag == "Player" && instigator.tag == "Enemy") || (target.tag == "Enemy" && instigator.tag == "Player"))
            {
                target.GetComponent<Health>().TakeDamage(damage);

                if (target.tag == "Player")
                {
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    audioSource.PlayOneShot(target.GetComponent<PlayerController>().hurtSound);
                }

                if (target.tag == "Enemy")
                {

                    if (target.gameObject != null)
                    {
                        AudioSource audioSource = target.GetComponent<AudioSource>();

                        if (target.GetComponent<EnemyChaser>() != null)
                        {
                            audioSource.PlayOneShot(target.GetComponent<EnemyChaser>().enemyHurtSound);
                        }
                        else
                        {
                            audioSource.PlayOneShot(target.GetComponent<EnemyFixed>().enemyHurtSound);
                        }
                    }

                }

            }
        }

    }

}