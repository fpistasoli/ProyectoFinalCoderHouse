using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Bonfire.Control
{
    public class MovingPlatformController : MonoBehaviour
    {

        [SerializeField] private UnityEvent<float> onMovePlatform;
        [SerializeField] Transform endPoint;
        private Vector3 startPoint;
        private float platformSpeed = 2.0f;
        private bool isMoving = false;
        private float delta = 0.1f;


        // Start is called before the first frame update
        void Start()
        {
            startPoint = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
           //Debug.Log("LA PLATAFORMA SE ESTA MOVIENDO: " + isMoving);
            //Debug.Log("ENDPOINT: " + endPoint.position);
            if(isMoving)
            {
                transform.Translate((endPoint.position - transform.position).normalized * platformSpeed * Time.deltaTime);
                if(Vector3.Distance(transform.position, endPoint.position) < delta )
                {
                    isMoving = false;
                    endPoint.position = startPoint;
                    //Debug.Log("ENDPOINT: " + endPoint.position);
                    startPoint = transform.position;
                }
            }
        }


        public void OnMovePlatformHandler(float platformSpeed)
        {
            //Debug.Log(this + " recibio el evento onMovePlatform");
            isMoving = true;

            SetPlayerAsChildOfPlatform();
        }

        private void SetPlayerAsChildOfPlatform()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.SetParent(transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
               // Debug.Log(this + " llamo al evento onMovePlatform");
                onMovePlatform?.Invoke(platformSpeed);
            }
        }


        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.parent = null;
              
            }
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, endPoint.position);

        }


    }
}
