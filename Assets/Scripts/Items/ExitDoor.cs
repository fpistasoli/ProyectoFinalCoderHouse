using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Bonfire.Core;
using Bonfire.Control;

namespace Bonfire.Items
{

    public class ExitDoor : MonoBehaviour
    {

        private bool isOpen = false;
        //[SerializeField] private GameObject[] pointLights;

        //EVENTS
        public static event Action onRestoreStats;
        public static event Action onGameWon;



        void Start()
        {

        }
      
        void Update()
        {
            Debug.Log("OPEN? " + isOpen);
         
        }

        public void Open()
        {
            isOpen = true;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (isOpen)
            {
                if (other.gameObject.tag == "Player")
                {
                    int currentLevel = GameManager.sharedInstance.GetCurrentLevel();

                    if(currentLevel == 3)
                    {
                        //GANE EL JUEGO
                        Debug.Log("YOU WON!");
                        onGameWon?.Invoke();
                    }
                    else
                    {
                        Debug.Log("ENTRO EN LOAD SCENE");
                        SceneManager.LoadScene(currentLevel + 1);
                    }

                    onRestoreStats?.Invoke();

                }
            }
        }


        /*
        public void TurnLightOn(Light pointLight) //CORREGIR PARA QUE APAREZCAN LAS LUCES EN LA EXITDOOR
        {

            //Debug.Log("LIGHT DEL BONFIRE ES: " + pointLight);

            //Debug.Log("LA CANT DE POINT LIGHTS DEL EXIT DOOR SON: " + pointLights.Length);

            foreach (GameObject lightPrefab in pointLights)
            {
                //Debug.Log("LIGHT PREFAB EN EXITDOOR: " + lightPrefab.GetComponent<Light>());
                //Debug.Log("LIGHT FROM BONFIRE: " + pointLight);
                if (pointLight.color == lightPrefab.GetComponent<Light>().color)
                {
                    lightPrefab.GetComponent<Light>().enabled = true;

                    //Debug.Log("SE ENCENDIO LA LUZ " + lightPrefab.name);
                    break;
                }
            }
            

        }
        */


    }

}