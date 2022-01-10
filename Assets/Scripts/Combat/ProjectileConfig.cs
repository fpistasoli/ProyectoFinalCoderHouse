using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Combat
{
    public class ProjectileConfig : MonoBehaviour
    {
        public static ProjectileConfig sharedInstance;
        private float damage;

        
        private void Awake() //SINGLETON
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; //this se refiere a la instancia de esta clase
                damage = 1.0f;
                //DontDestroyOnLoad(gameObject); //no quiero que persista en todas las escenas
            }
            else
            {
                Destroy(gameObject);
            }

        }
        

        public float GetDamage()
        {
            return damage;
        }

        public void DamagePowerLevelUp(float powerBoost)
        {
            damage += powerBoost;
        }



    }
}
