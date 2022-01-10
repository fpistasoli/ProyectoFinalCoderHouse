using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bonfire.Attributes;
using Bonfire.Items;
using Bonfire.Control;

namespace Bonfire.Core
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState {InTheGame, GameOver, EndOfGame, MainMenu}
        private GameState gameState;

        [SerializeField] private int numberOfLevels = 3;
        public static GameManager sharedInstance;
        private static int numberOfInactiveBonfires;
        public static int score;
        private int currentLevel;

        private void Awake() //SINGLETON
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; //this se refiere a la instancia de esta clase
                score = 0;
                DontDestroyOnLoad(gameObject); //para que el objeto GameManager, que tiene a esta clase como componente, persista de escena en escena
            }
            else
            {
                Destroy(gameObject);
            }

            BonfireSpawner.onLitBonfiresUpdate += UpdateNumberOfInactiveBonfiresHandler;
            //Health.onUnlightBonfires += OnUnlightBonfiresHandler;

        }



        void Start()
        {
            numberOfInactiveBonfires = GetInitialNumberOfInactiveBonfires();
            currentLevel = 1;

            Health.onDeath += GameOverHandler;
            Health.onDeath += RestoreStatsHandler;
            //PlayerController.onDeathFallOffWorld += GameOverHandler;
            ExitDoor.onRestoreStats += RestoreStatsHandler;
            
            gameState = GameState.InTheGame;

        }

        void Update()
        {

            Debug.Log("CURRENT GAME STATE: " + GetCurrentGameState());


            //Debug.Log("SCORE: " + score);
            Debug.Log("REMAINING BONFIRES: " + numberOfInactiveBonfires);


            if (numberOfInactiveBonfires == 0)
            {
                OpenExitDoor();

                //ir al prox nivel

            }

        }


        void OnDestroy()
        {
            BonfireSpawner.onLitBonfiresUpdate -= UpdateNumberOfInactiveBonfiresHandler;
        }


        public GameState GetCurrentGameState()
        {
            return gameState;
        }

        public void SetCurrentGameState(GameState newGameState)
        {
            gameState = newGameState;
        }


        private void RestoreStatsHandler()
        {
            numberOfInactiveBonfires = GetInitialNumberOfInactiveBonfires();

            if (currentLevel == numberOfLevels)
            {
                currentLevel = 1;
            }
            else
            {
                currentLevel++;
            }

        }

        private void GameOverHandler()
        {
            //Debug.Log(this + " recibio el evento onDeath");
            //score = 0;
            gameState = GameState.GameOver;
            //RestoreStatsHandler();
        }


        private int GetInitialNumberOfInactiveBonfires()
        {
            GameObject[] bonfires = GameObject.FindGameObjectsWithTag("Bonfire");
            return bonfires.Length;
        }


        private void OpenExitDoor()
        {
            GameObject exitDoor = GameObject.FindWithTag("Exit");
            exitDoor.GetComponent<ExitDoor>().Open();
        }

        public static int GetNumberOfInactiveBonfires()
        {
            return numberOfInactiveBonfires;

        }

        public void UpdateNumberOfInactiveBonfiresHandler()
        {
            //Debug.Log(this + " recibio el evento onLitBonfiresUpdate");

            if (numberOfInactiveBonfires > 0)
            {
                numberOfInactiveBonfires--;
            }
            else
            {
                //gane el nivel
                //abrir puerta de salida del nivel
            }

        }

        private void OnUnlightBonfiresHandler()
        {
            //Debug.Log(this + " recibio el evento onUnlightBonfires");
            numberOfInactiveBonfires = 5;
            GameObject[] allFires = GameObject.FindGameObjectsWithTag("Fire");

            foreach(GameObject fire in allFires)
            {
                //GameObject bonfireParent = fire.transform.parent.gameObject.transform.parent.gameObject;
                //bonfireParent.GetComponent<BonfireItem>().Unlight();
                Destroy(fire);
            }
            
            PutOutAllBonfires();

        }

        private void PutOutAllBonfires()
        {
            GameObject[] allBonfires = GameObject.FindGameObjectsWithTag("Bonfire");

            foreach(GameObject bonfire in allBonfires)
            {
                bonfire.GetComponent<BonfireItem>().Unlight();
            }
           
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

    }
}
