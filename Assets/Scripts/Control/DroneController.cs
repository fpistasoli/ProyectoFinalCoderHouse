using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

    [SerializeField] float distanceRay = 20f;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private int shootCooldown = 2;
    [SerializeField] private float shootTime = 2.0f;
    [SerializeField] private float shotSpeed = 1.0f;

    [SerializeField] private GameObject snowballPrefab;

    private bool canShoot = true;
    private bool toggleDrone = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleDrone)
        {
            if (canShoot)
            {
                RaycastDroneLaser();
            }
            else
            {
                shootTime += Time.deltaTime;
            }

            if (shootTime > shootCooldown)
            {
                canShoot = true;
            }
        }
    }

    private void RaycastDroneLaser()
    {
        //Debug.Log("GENERANDO EL RAYCAST");
        RaycastHit hit;

        bool hasHit = Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, distanceRay);
        
        if (hasHit)
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("EL RAYO CHOCA AL PLAYER");
                canShoot = false; //ya dispare una vez, asi que no puedo disparar
                shootTime = 0;
                GameObject newSnowball = Instantiate(snowballPrefab, shootPoint.transform.position, snowballPrefab.transform.rotation);
                newSnowball.GetComponent<Rigidbody>().AddForce(shootPoint.transform.TransformDirection(Vector3.forward) * shotSpeed, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(toggleDrone)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
   
        Gizmos.DrawRay(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward) * distanceRay);
    }

    public void ToggleDrone()
    {
        Debug.Log(this + " recibio el evento onSwitchDrones");
        toggleDrone = !toggleDrone;
    }



}
