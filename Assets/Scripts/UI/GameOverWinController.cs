using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bonfire.Items;
using Bonfire.Core;

namespace Bonfire.UI
{
    public class GameOverWinController : MonoBehaviour
    {

        [SerializeField] private Text gameWonText;
        [SerializeField] private Button backToMainMenuButton;

        private void Awake()
        {
            backToMainMenuButton.gameObject.SetActive(false);
        }


        private void Start()
        {
            ExitDoor.onGameWon += OnGameWonHandler;
          
        }

        private void OnGameWonHandler()
        {
            GameManager.sharedInstance.SetCurrentGameState(GameManager.GameState.EndOfGame);
            gameWonText.text = "YOU WON THE GAME!";
            backToMainMenuButton.gameObject.SetActive(true);
            //Time.timeScale = 0;

            //Destroy(GameObject.Find("HUD")); //ya que lo vuelvo a instanciar en el Level 1

        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
            //Time.timeScale = 1;
        }
        

    }



}
