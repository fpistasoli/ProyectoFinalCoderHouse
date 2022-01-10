using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using Bonfire.Combat;
using Bonfire.Control;
using System;

namespace Bonfire.Items
{
    public class InventoryManager : MonoBehaviour
    {

        public static InventoryManager sharedInstance;

        private List<GameObject> inventoryHB; //health boosts
        private List<GameObject> inventoryPB; //power boosts

        private Health health;
        private Projectile projectile;

        private void Awake() //SINGLETON
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; //this se refiere a la instancia de esta clase
                //DontDestroyOnLoad(gameObject); //no quiero que persista en todas las escenas
            }
            else
            {
                Destroy(gameObject);
            }

        }
        

        void Start()
        {
            ExitDoor.onRestoreStats += RestoreInventory;

            inventoryHB = new List<GameObject>();
            inventoryPB = new List<GameObject>();

            GameObject player = GameObject.FindWithTag("Player");

            health = player.GetComponent<Health>();
            projectile = player.GetComponent<PlayerController>().GetProjectilePrefab().GetComponent<Projectile>();

        }

        private void RestoreInventory()
        {
            inventoryHB = new List<GameObject>();
            inventoryPB = new List<GameObject>();
        }

        void Update()
        {

        }


        public void AddInventoryHB(GameObject healthBoost)
        {
            inventoryHB.Add(healthBoost);
        }

        public void AddInventoryPB(GameObject powerBoost)
        {
            inventoryPB.Add(powerBoost);
        }


        public void UpgradeBoosts(GameObject upgrade)
        {
            float boost = upgrade.GetComponent<PickupController>().GetBoost();

            foreach (GameObject pickup in inventoryHB)
            {
                pickup.GetComponent<PickupController>().UpgradeBoost(boost);
            }

            foreach (GameObject pickup in inventoryPB)
            {
                pickup.GetComponent<PickupController>().UpgradeBoost(boost);
            }

        }

        public void UseInventoryHB()
        {
            int numberOfElements = inventoryHB.Count;
            GameObject element = null;
            if (numberOfElements > 0)
            {
                element = inventoryHB[numberOfElements - 1];
                inventoryHB.RemoveAt(numberOfElements - 1);

                GameObject playerGO = GameObject.FindWithTag("Player");
                AudioSource audioSource = playerGO.GetComponent<AudioSource>();
                audioSource.PlayOneShot(playerGO.GetComponent<PlayerController>().boostPickupSound);

            }

            float healBoost = element.GetComponent<PickupController>().GetBoost();
            health.Heal(healBoost);
        }

        public void UseInventoryPB()
        {
            int numberOfElements = inventoryPB.Count;
            GameObject element = null;
            if (numberOfElements > 0)
            {
                element = inventoryPB[numberOfElements - 1];
                inventoryPB.RemoveAt(numberOfElements - 1);

                GameObject playerGO = GameObject.FindWithTag("Player");
                AudioSource audioSource = playerGO.GetComponent<AudioSource>();
                audioSource.PlayOneShot(playerGO.GetComponent<PlayerController>().boostPickupSound);
            }

            PickupController pickupController = element.GetComponent<PickupController>();
            float powerBoost = pickupController.GetBoost();
            ProjectileConfig.sharedInstance.DamagePowerLevelUp(powerBoost);
        
        }

        public bool IsEmptyInventoryHB()
        {
            return inventoryHB.Count == 0;
        }

        public bool IsEmptyInventoryPB()
        {
            return inventoryPB.Count == 0;
        }

        public int GetNumberOfHealthBoosts()
        {
            return inventoryHB.Count;
        }
        public int GetNumberOfPowerBoosts()
        {
            return inventoryPB.Count;
        }

        public void PrintInventoryHB()
        {
            Debug.Log("HAY " + inventoryHB.Count + " ELEMENTOS EN INVENTORYHB");
            foreach (GameObject go in inventoryHB)
            {
                Debug.Log(go.GetComponent<PickupController>().GetPickupType() + " with boost " + go.GetComponent<PickupController>().GetBoost());
            }
        }
        public void PrintInventoryPB()
        {
            Debug.Log("HAY " + inventoryPB.Count + " ELEMENTOS EN INVENTORYPB");
            Debug.Log("Inventory PB: ");
            foreach (GameObject go in inventoryPB)
            {
                Debug.Log(go.GetComponent<PickupController>().GetPickupType() + " with boost " + go.GetComponent<PickupController>().GetBoost());
            }
        }


    }

}