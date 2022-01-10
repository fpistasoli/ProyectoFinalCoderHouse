using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bonfire.Control
{
    public class SwitchDrone : MonoBehaviour
    {
        [SerializeField] private UnityEvent onSwitchDrones;
        Material mat;
   
        private bool isOn = true;

        private void Start()
        {
            mat = GetComponent<Renderer>().material;
            mat.color = Color.green;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log(this + " llamo al evento onSwitchDrones");
                onSwitchDrones?.Invoke();

                isOn = !isOn;
                if (!isOn)
                {
                    mat.color = Color.red;
                }
                else
                {
                    mat.color = Color.green;
                }
            }
        }



    }

}