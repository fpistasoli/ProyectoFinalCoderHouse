using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bonfire.Core;
using System;

namespace Bonfire.UI
{
    public class LandingController : MonoBehaviour
    {

        [SerializeField] private InputField inputUsername;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject controlsPanel;
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject introPanel;
        [SerializeField] private Text introPanelText;
        [SerializeField] private Text loadingGameText;

        private bool controlsPanelActivated = false;
        private bool creditsPanelActivated = false;

        // Start is called before the first frame update
        void Start()
        {

            GameObject gameManagerGO = GameObject.Find("Game Manager");
            GameObject hudGO = GameObject.Find("HUD");

            if (gameManagerGO != null)
            {
                // GameManager.sharedInstance.SetCurrentGameState(GameManager.GameState.MainMenu);
                Destroy(gameManagerGO); //porque se crea en LEVEL 1
            }

            if (hudGO != null) 
            { 
                Destroy(hudGO); 
            }

        }


        // Update is called once per frame
        void Update()
        {

        }

        public void OnChangeInputUsername() //se llama al ir ingresando texto en el inputfield
        {
            Debug.Log("CHANGE"); 
        }

        public void OnEndEditInputUsername() //se llama al terminar de ingresar el texto en el inputfield
        {
            if (inputUsername.text != null)
            {
                ProfileManager.sharedInstance.SetPlayerName(inputUsername.text);
            }

        }

        public void OnClickNewGame()
        {
            LoadIntroAndGame();
        }

        private void LoadIntroAndGame()
        {
            //desactivo paneles y activo el intro panel
            introPanel.SetActive(true);
            mainPanel.SetActive(false);
            if (controlsPanel.activeSelf) { controlsPanel.SetActive(false); }
            if (creditsPanel.activeSelf)  { creditsPanel.SetActive(false); }

            //show intro & wait before loading game
            StartCoroutine(ShowIntro());
        }

        private IEnumerator ShowIntro()
        {
            yield return new WaitForSeconds(1.0f);
            introPanelText.text = "Hello " + inputUsername.text + "!" + System.Environment.NewLine;
            yield return new WaitForSeconds(2.0f);
            introPanelText.text += "You are the only survivor of the frosty land of the Zlorps. Your mission is to light up all the five bonfires in each level. " + System.Environment.NewLine;
            yield return new WaitForSeconds(6.0f);
            introPanelText.text += "This will open the exit door of the stage to which you must reach before getting frostbite. " + System.Environment.NewLine;
            yield return new WaitForSeconds(5.0f);
            introPanelText.text += "To aid you in your quest, the bonfires and red bottles will give you health boosts, while the green bottles will give you power boosts." + System.Environment.NewLine;
            yield return new WaitForSeconds(5.0f);
            introPanelText.text += "Good luck!";
            yield return new WaitForSeconds(2.0f);
            loadingGameText.text = "LOADING...";
            yield return new WaitForSeconds(3.0f);

            StartGame();

        }

        public void StartGame()
        {
            if (GameObject.Find("Game Manager") != null)
            {
                GameManager.sharedInstance.SetCurrentGameState(GameManager.GameState.InTheGame);
            }

            SceneManager.LoadScene(1);
        }

        public void OnClickControls()
        {
            controlsPanel.SetActive(!controlsPanelActivated);
            controlsPanelActivated = !controlsPanelActivated;
        }

        public void OnClickCredits()
        {
            creditsPanel.SetActive(!creditsPanelActivated);
            creditsPanelActivated = !creditsPanelActivated;
        }




        //public void OnChangeToggleName(bool newStatus)
        //{
        //}

    }

}