using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ShootBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShootBullet()
    {
        Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
    }



}
