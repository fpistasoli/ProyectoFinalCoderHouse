using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bonfire.Control
{
    public class VentController : MonoBehaviour
    {

        [SerializeField] private UnityEvent<float> onSoar;
        private float soaringForceMagnitude = 10.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log(this + " llamo al evento onSoar");
                onSoar?.Invoke(soaringForceMagnitude);

            }
        }

        public float GetSoaringForceMagnitude()
        {
            return soaringForceMagnitude;
        }


    }
}
