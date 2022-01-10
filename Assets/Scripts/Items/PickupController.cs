using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Items
{
    public class PickupController : MonoBehaviour
    {
        
        [SerializeField] private PickupType pickupType;
        [SerializeField] private float boost;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public PickupType GetPickupType()
        {
            return pickupType;
        }

        public float GetBoost()
        {
            return boost;
        }

        public void UpgradeBoost(float boost)
        {
            this.boost += boost;

        }
            
    }
}
