using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bonfire.Attributes;
using Bonfire.Core;
using Bonfire.Items;
using Bonfire.Control;

namespace Bonfire.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Text healthPointsValue;
        [SerializeField] private Text levelValue;
        [SerializeField] private Text bonfiresValue;
        [SerializeField] private Text healthBoostValue;
        [SerializeField] private Text powerBoostValue;
        [SerializeField] private Text gameOverText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;

        public static HUDController sharedInstance;


        private void Awake() //SINGLETON
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; //this se refiere a la instancia de esta clase
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }



        void Start()
        {
            healthPointsValue.text = "100 %";
            levelValue.text = "1";
            bonfiresValue.text = "0/5";
            healthBoostValue.text = "0";
            powerBoostValue.text = "0";

            Health.onDeath += OnDeathHandler;
            //PlayerController.onDeathFallOffWorld += OnDeathHandler;
            BonfireSpawner.onLitBonfiresUpdate += OnLitBonfiresUpdateHandler;
            //Health.onUnlightBonfires += OnUnlightBonfireHandler;
            ExitDoor.onRestoreStats += RestoreBonfiresValue;

            playAgainButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);

        }


        //private void OnUnlightBonfireHandler()
        //{
        //    Debug.Log(this + " recibio el evento onUnlightBonfires");
        //    bonfiresValue.text = "0/5";
        //}

        void Update()
        {

            if (GameManager.sharedInstance.GetCurrentGameState() == GameManager.GameState.InTheGame)
            {
                UpdateHealthPointsUI();
                UpdateCurrentLevelUI();
                //UpdateActiveBonfiresUI();
                UpdateNumberOfHealthBoostsUI();
                UpdateNumberOfPowerBoostsUI();
                //Update de cada cosa
                //Debug.Log("INACTIVE BONFIRES: " + GameManager.GetNumberOfInactiveBonfires());
                Debug.Log("BONFIRES VALUE: " + bonfiresValue);
            }

        }

        void OnDestroy()
        {
            BonfireSpawner.onLitBonfiresUpdate -= OnLitBonfiresUpdateHandler;
            Health.onDeath -= OnDeathHandler;
            ExitDoor.onRestoreStats -= RestoreBonfiresValue;
        }

        private void OnDeathHandler()
        {
            if(GameManager.sharedInstance.GetCurrentGameState() == GameManager.GameState.GameOver)
            {
                Debug.Log(this + " recibio el evento onDeath");
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "GAME OVER";
                playAgainButton.gameObject.SetActive(false);
                mainMenuButton.gameObject.SetActive(true);
                //Time.timeScale = 0;
            }
        }

        private void OnLitBonfiresUpdateHandler()       
        {
            Debug.Log(this + " recibio el evento onLitBonfiresUpdate");
            Debug.Log("number of inactive bonfires: " + GameManager.GetNumberOfInactiveBonfires());
            bonfiresValue.text = (5 - GameManager.GetNumberOfInactiveBonfires()).ToString() + "/5";
        }

        private void RestoreBonfiresValue()
        {
            bonfiresValue.text = "0/5";
        }


        private void UpdateCurrentLevelUI()
        {
            levelValue.text = GameManager.sharedInstance.GetCurrentLevel().ToString();
        }

        
        private void UpdateHealthPointsUI()
        {
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log("PLAYER: " + player); 
            Health playerHealth = player.GetComponent<Health>();
            Debug.Log("HEALTH: " + playerHealth);
            healthPointsValue.text = Mathf.CeilToInt(playerHealth.GetHealthPoints()).ToString() + "%";
        }




        public void UpdateNumberOfHealthBoostsUI()
        {
            healthBoostValue.text = InventoryManager.sharedInstance.GetNumberOfHealthBoosts().ToString();
        }

        private void UpdateNumberOfPowerBoostsUI()
        {
            powerBoostValue.text = InventoryManager.sharedInstance.GetNumberOfPowerBoosts().ToString();
        }

        public void BackToMainMenu()
        {
            Debug.Log("PRESIONE BACKTOMAINMENU");
            //Destroy(GameObject.Find("HUD")); //ya que lo vuelvo a instanciar en el Level 1
            SceneManager.LoadScene(0);
            //Time.timeScale = 1;
            
        }

        public void PlayAgain()
        {
            Debug.Log("PRESIONE PLAYAGAIN");
            GameManager.sharedInstance.SetCurrentGameState(GameManager.GameState.InTheGame);
            SceneManager.LoadScene(GameManager.sharedInstance.GetCurrentLevel());
            HideGameOverButtonsAndText();


            //Time.timeScale = 1;
        }

        private void HideGameOverButtonsAndText()
        {
            playAgainButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            Debug.Log("OCULTO BOTONES GAME OVER");
            GameManager.sharedInstance.SetCurrentGameState(GameManager.GameState.InTheGame);



        }
    }

}