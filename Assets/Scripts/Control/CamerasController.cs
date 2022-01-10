using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Control
{
    public class CamerasController : MonoBehaviour
    {

        public GameObject[] cameras;

        public void Awake()
        {
            OnlyActivateCamera(0); //activa solo la follow camera
        }


        void Start()
        {
            
        }

    
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.F1))
            {
               OnlyActivateCamera(0);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                OnlyActivateCamera(1);
            }


        }


        private void OnlyActivateCamera(int j)
        {

            for(int i=0; i < cameras.Length ; i++)
            {
                if (i!=j)
                {
                    cameras[i].SetActive(false);
                } else
                {
                    cameras[i].SetActive(true);
                }
            }

        }

    }



}
