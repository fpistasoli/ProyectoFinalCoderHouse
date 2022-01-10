using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Bonfire.Combat;
using Bonfire.Items;
using Bonfire.Attributes;
using Bonfire.Enemies;
using Bonfire.Core;

namespace Bonfire.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private float speed = 3.0f; //SerializeField permite cambiar una variable privada desde el inspector, pero no permite modificar desde otras clases
        [SerializeField] private float rotationSpeed = 100.0f;
        [SerializeField] private GameObject bowPrefab;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject shootPoint;
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject fireSpawner;
        [SerializeField] private Animator animatorController;
        [SerializeField] private Transform camTransform;
        [SerializeField] private float shotDelayTime = 1.0f;
        [SerializeField] private float deathHeight = 10.0f;

        Health health;

        private bool isMoving = false;
        private bool isTurning = false;
        private bool isAttacking = false;

        private float translation;
        private float rotation;
        private Vector3 translationVelocity;
        private Vector3 rotationVelocity;

        private Rigidbody rbPlayer;

        //CAMERA ROTATION PARAMETERS
        [SerializeField] private float speedH = 2.0f;
        [SerializeField] private float speedV = 2.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        //EVENTOS
        public static event Action<bool> onDroneChange;
        public static event Action<bool> onSwitchDroneChange;
        public static event Action onDeathFallOffWorld;
        public static event Action<bool> onMistGenerator;
        public static event Action<bool> onBossPoint;

        //SOUND EFFECTS
        //[SerializeField] AudioClip footstepsSound;
        [SerializeField] public AudioClip hurtSound;
        [SerializeField] public AudioClip boostPickupSound;
        [SerializeField] public AudioClip bonfireSound;
        [SerializeField] public AudioClip allBonfiresSound;
        [SerializeField] AudioClip arrowShotShound;
        private AudioSource audioSource;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        void Start()
        {
            rbPlayer = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();


            Health.onDeath += Die;

            //Transform leftHandTransform = leftHand.transform;
            //GameObject bow = Instantiate(bowPrefab, leftHandTransform.position, bowPrefab.transform.rotation);


        }

        private void Die()
        {
            //GetComponent<PlayerController>().enabled = false;
            //die animation
        }


        void Update()
        {
            Debug.Log("PLAYER OBJECT: " + gameObject);
            if(GameManager.sharedInstance.GetCurrentGameState() == GameManager.GameState.InTheGame)
            {
                InteractWithMovement();
                InteractWithCombat();
                InteractWithItems();
                UpdateAnimator();
                FallOffWorld();
            }
        }

        private void FallOffWorld()
        {
            if(transform.position.y <= -deathHeight)
            {
                //onDeathFallOffWorld?.Invoke();
                health.TakeDamage(100.0f);
            }
        }

        private bool IsMouseOverUI()
        {
            bool isOverUI = false;

            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResultList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

            for(int i=0; i < raycastResultList.Count; i++)
            {
                if(raycastResultList[i].gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    isOverUI = true;
                    break;
                }
            }

            return isOverUI;
        }



        public GameObject GetProjectilePrefab()
        {
            return projectilePrefab;
        }

    
        private void InteractWithItems()
        {

            if (Input.GetKeyDown(KeyCode.Q) && !InventoryManager.sharedInstance.IsEmptyInventoryHB())
            {
                InventoryManager.sharedInstance.UseInventoryHB();
            }

            if (Input.GetKeyDown(KeyCode.E) && !InventoryManager.sharedInstance.IsEmptyInventoryPB())
            {
                InventoryManager.sharedInstance.UseInventoryPB();
            }
            
        }


        private void InteractWithMovement()
        {
            // Get the horizontal and vertical axis.
            // By default they are mapped to the arrow keys.
            // The value is in the range -1 to 1
            translation = Input.GetAxis("Vertical") * speed;

            rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            if (translation != 0)
            {
                isMoving = true;
            }

            if (rotation != 0)
            {
                isTurning = true;
            }

            // Make it move 10 meters per second instead of 10 meters per frame...
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            // Move translation along the object's z-axis
            translationVelocity = new Vector3(0, 0, translation);
            transform.Translate(translationVelocity);

            // Rotate around our y-axis
            rotationVelocity = new Vector3(0, rotation, 0);
            transform.Rotate(rotationVelocity);

            //Rotate camera with player's rotation
            camTransform.Rotate(rotationVelocity);

            //if (isMoving) { PlayFootstepsSound(); }

        }

        private void InteractWithCombat()
        {

            if (Input.GetButtonDown("Fire1"))
            {
                
                StartCoroutine(ArrowShot(shotDelayTime));

                //chequeo si el proyectil impacto con un enemy: (SE HACE CON UN ONTRIGGER

                /*
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Collider collider = hit.collider;
                    if (collider.CompareTag("Enemy"))
                    {
                        //Debug.Log("LA BALA CHOCO CONTRA EL ENEMIGO");
                        //float projectileDamage = projectile.GetComponent<Projectile>().GetDamage();
                        float projectileDamage = ProjectileConfig.sharedInstance.GetDamage();

                        //Debug.Log("EL DA�O DEL PROYECTIL ES: " +  projectileDamage);

                        collider.gameObject.GetComponent<Health>().TakeDamage(projectileDamage);
                    }

                }
                */
            }

        }

        private IEnumerator ArrowShot(float shotDelayTime)
        {
            isAttacking = true;
            yield return new WaitForSeconds(shotDelayTime);

            //shootPoint.transform.TransformDirection(Vector3.forward);

            GameObject arrow = Instantiate(projectilePrefab, shootPoint.transform.position, projectilePrefab.transform.rotation);
      
            //float angleToRotate = Vector3.Angle(arrow.transform.right, shootPoint.transform.right);

            //Debug.Log(angleToRotate);

            //arrow.transform.rotation = Quaternion.AngleAxis(angleToRotate, shootPoint.transform.forward);

            arrow.GetComponent<Projectile>().SetProjectileDirection(transform.forward);
            arrow.GetComponent<Projectile>().SetInstigator(gameObject);

            audioSource.PlayOneShot(arrowShotShound);


        }

        private void UpdateAnimator()
        {
            if (isAttacking)
            {
                animatorController.SetTrigger("isAttacking");
                Debug.Log("ATTACKING");
            }

            if (isMoving) // y/o girar
            {
                //animatorController.SetFloat("Forward", 1.0f);
                //animatorController.SetFloat("Turn", 0.0f);

                animatorController.SetBool("isRunning", true);
                Debug.Log("MOVING");

    

            } else { // no se esta moviendo

                animatorController.SetBool("isRunning", false);

                if (isTurning)
                {
                    //animatorController.SetFloat("Turn", 1.0f);
                } else { // idle
                    //animatorController.SetFloat("Turn", 0.0f);
                }

                /*if (isAttacking)
                {
                    animatorController.SetBool("isAttacking", true);
                    Debug.Log("ATTACKING");
                }*/


            }

            isMoving = false;
            isTurning = false;
            isAttacking = false;

        }


        public void OnSoarHandler(float forceMagnitude)
        {
            //Debug.Log(this + " recibio el evento onSwitchDrones");
            rbPlayer.AddForce(Vector3.up * forceMagnitude, ForceMode.Impulse);
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy Right Hand")
            {
                EnemyRightHand enemyRightHandScript = collision.gameObject.GetComponent<EnemyRightHand>();
                if (enemyRightHandScript != null)
                {
                    GameObject enemyRootPrefab = enemyRightHandScript.GetEnemyRoot();
                    float punchDamage = enemyRootPrefab.GetComponent<EnemyChaser>().GetEnemyData().GetPunchDamage();
                    health.TakeDamage(punchDamage);

                    audioSource.PlayOneShot(hurtSound);


                   // Debug.Log("PUNCH DAMAGE: " + punchDamage);
                }
            }

            if (collision.gameObject.tag == "Drone")
            {

                onDroneChange?.Invoke(true);

            }

            if (collision.gameObject.tag == "SwitchDrone")
            {

                onSwitchDroneChange?.Invoke(true);

            }

        }



        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Drone")
            {

                onDroneChange?.Invoke(false);

            }

            if (collision.gameObject.tag == "SwitchDrone")
            {

                onSwitchDroneChange?.Invoke(false);

            }

        }





            private void OnTriggerStay(Collider other)
        {
            //Debug.Log("SE DISPARO EL ONTRIGGER STAY");

            if (other.gameObject.tag == "Bonfire")
            {
                GameObject bonfire = other.gameObject;
                Transform fireSpawnerPoint = bonfire.transform.GetChild(0);

                //Debug.Log("LA FOGATA NO ESTA ENCENDIDA AUN");

                if (Input.GetButtonDown("Fire2")) //encender fogata
                {
                    if (!(other.gameObject.GetComponent<BonfireItem>().IsOn())) //si no esta encendida la fogata, puedo encenderla
                    {
                        Instantiate(fireSpawner, fireSpawnerPoint.position, fireSpawner.transform.rotation);
                        bonfire.GetComponent<BonfireItem>().Light();
                    }

                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Boost"))
            {
               // Debug.Log("ME CHOQUE CON UN BOOST PICKUP");
                other.gameObject.SetActive(false);

                PickupType pickupType = other.GetComponent<PickupController>().GetPickupType();
                GameObject pickup = other.gameObject;

                switch (pickupType)
                {
                    case PickupType.HealthBoost:
                        InventoryManager.sharedInstance.AddInventoryHB(pickup);
                        break;

                    case PickupType.PowerBoost:
                        InventoryManager.sharedInstance.AddInventoryPB(pickup);
                        break;

                    case PickupType.Upgrade:
                        InventoryManager.sharedInstance.UpgradeBoosts(pickup);
                        //UpgradeBoostsOfPickupsInScene(pickup);
                        break;
                }

                InventoryManager.sharedInstance.PrintInventoryHB();

                InventoryManager.sharedInstance.PrintInventoryPB();



            }



            if (other.gameObject.tag == "MistGenerator")
            {

                onMistGenerator?.Invoke(true);

            }



            if (other.gameObject.tag == "BossPoint")
            {

                onBossPoint?.Invoke(true);

            }


        }



        public void OnMovePlatformHandler(float platformSpeed)
        {
           // Debug.Log(this + " recibio el evento onMovePlatform");




            //ir con la velocidad a la que se mueve el platform

        }




        /*
        private void UpgradeBoostsOfPickupsInScene(GameObject pickup)
        {
            float boost = pickup.GetComponent<PickupController>().GetBoost();

            GameObject[] allItemsChildrenInScene = GameObject.FindGameObjectsWithTag("Boost");
            GameObject[] allItemsInScene = new GameObject[allItemsChildrenInScene.Length];

            Debug.Log("HAY " + allItemsInScene.Length + " ITEMS ACTIVOS EN LA ESCENA");

            for (int i = 0; i < allItemsInScene.Length; i++)
            {
                allItemsInScene[i] = allItemsChildrenInScene[i].transform.parent.gameObject;
            }

            foreach (GameObject go in allItemsInScene)
            {
                go.GetComponent<PickupController>().UpgradeBoost(boost);
            }
           
        }
        */


        /*
        private void PlayFootstepsSound()
        {
            footstepsSound.Play();

        }
        */

    }

}