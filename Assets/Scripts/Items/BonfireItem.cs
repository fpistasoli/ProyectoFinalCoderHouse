using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using Bonfire.Control;
using System;

namespace Bonfire.Items
{
    public class BonfireItem : MonoBehaviour
    {

        //[SerializeField] private Light pointLight;
        [SerializeField] private GameObject exitDoorPrefab;
        [SerializeField] private GameObject lightPrefab;    


        private bool isOn;

        void Start()
        {
            isOn = false;
            //Health.onUnlightBonfires += OnUnlightBonfireHandler;
        }

        private void OnUnlightBonfireHandler()
        {
            Debug.Log(this + " recibio el evento onUnlightBonfires");
            isOn = false;
        }

        void Update()
        {
            //Debug.Log(this.name + "ESTA ENCENDIDA: " + isOn);
        }

        public bool IsOn()
        {
            return isOn;
        }

        public void Light()
        {
            isOn = true;

            GameObject playerGO = GameObject.FindWithTag("Player");
            AudioSource audioSource = playerGO.GetComponent<AudioSource>();
            audioSource.PlayOneShot(playerGO.GetComponent<PlayerController>().bonfireSound);

            //Debug.Log("SE ENCENDIO LA HOGUERA");

            //Debug.Log("LA LUZ DE ESTE BONFIRE ES: " + lightPrefab); 

            //exitDoorPrefab.GetComponent<ExitDoor>().TurnLightOn(lightPrefab.GetComponent<Light>());
        }

        public void Unlight()
        {
            isOn = false;
        }

        //public Light GetPointLight()
        //{
       //     return pointLight;
       // }

    }


}



