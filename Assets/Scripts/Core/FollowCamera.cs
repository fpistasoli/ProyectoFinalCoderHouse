using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Core
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target; //player
  
        void Update()
        {
            //create a fixed follow camera on the player's movement
            transform.position = target.position; 


        }
    }




}


