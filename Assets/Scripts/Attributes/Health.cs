using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Core;
using Bonfire.Control;
using Bonfire.Enemies;
using Bonfire.Items;
using System;

namespace Bonfire.Attributes
{

    public class Health : MonoBehaviour
    {
        
        [SerializeField] private float healthPoints;

        private float maxHealthPoints = 100f;
        private bool isDead = false;
        private float playerHealthDeclineRate;
        //private bool windBlow = true;


        //EVENTS
        public static event Action onDeath;
        //public static event Action onUnlightBonfires; //al llegar a 50% de hp se apagan automaticamente todos los bonfires encendidos


        void Start()
        {
            if (this.transform.gameObject.tag == "Player")
            {
                healthPoints = maxHealthPoints;
            }

            ExitDoor.onRestoreStats += RestoreHPHandler;

        }

        private void RestoreHPHandler()
        {
            healthPoints = maxHealthPoints;
        }

        void Update()
        {
            if (this.transform.gameObject.tag == "Player")
            {
                playerHealthDeclineRate = GameManager.GetNumberOfInactiveBonfires();
                RunHealthDeclineTimer();

                /*if(windBlow && healthPoints<= 50.0f)
                {
                    Debug.Log(this + " llamo al evento onUnlightBonfires");
                    onUnlightBonfires?.Invoke();
                    windBlow = false;
                }
                */

                if(healthPoints<=0)
                {
                    onDeath?.Invoke();
                }


            }

        }




        private void RunHealthDeclineTimer()
        {
            if (this.transform.gameObject.tag == "Player")
            {
                healthPoints -= Time.deltaTime * playerHealthDeclineRate / 6;
                //Debug.Log("HP: " + Mathf.CeilToInt(healthPoints) + " %");
            }

            if (healthPoints <= 0)
            {
                healthPoints = 0;
            }
        }


        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); //para que health me quede en 0 cuando se queda sin vida

            if (healthPoints <= 0)
            {

                GameObject gameObjectPrefab = this.transform.gameObject;

                /*if (gameObjectPrefab.tag == "Player")
                {
                    //Debug.Log(this + " llamo al evento onDeath");
                    onDeath?.Invoke();
                }*/

                if (gameObjectPrefab.tag == "Enemy")
                {
                    GameManager.score += gameObjectPrefab.GetComponent<Enemy>().GetRewardPoints();
                    Destroy(gameObject);

                    /*
                    AudioClip audioClip;
                    if (gameObjectPrefab.GetComponent<EnemyChaser>() != null)
                    {
                        audioClip = gameObjectPrefab.GetComponent<EnemyChaser>().enemyDeathSound;
                    }
                    else
                    {
                        audioClip = gameObjectPrefab.GetComponent<EnemyFixed>().enemyDeathSound;
                    }

                    gameObjectPrefab.GetComponent<AudioSource>().PlayOneShot(audioClip);
                    */
                    
                }

                //Die();

            }
    
        }

        public void Heal(float healthToRestore)
        {
            healthPoints = Mathf.Min(healthPoints + healthToRestore, GetMaxHealthPoints());

        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return maxHealthPoints;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            //GetComponent<Animator>().SetTrigger("die");
   
        }

    }

}

