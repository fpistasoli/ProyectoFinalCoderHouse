using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Bonfire.Core;

namespace Bonfire.Items
{

    public class BonfireSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject bonfireItem;

        //EVENTS
        public static event Action onLitBonfiresUpdate;

        void Start()
        {
            //bonfireItem.GetComponent<BonfireItem>().Light();
            Debug.Log(this + " llamo al evento onLitBonfiresUpdate");
            onLitBonfiresUpdate?.Invoke();


            //GameManager.UpdateNumberOfInactiveBonfires();

        }

        void Update()
        {


        }


    }

}