using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Core
{
    public class ProfileManager : MonoBehaviour
    {

        public static ProfileManager sharedInstance;
        [SerializeField] private string playerName;
        private void Awake() //SINGLETON
        {
            if (sharedInstance == null)
            {
                sharedInstance = this; 
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }

        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPlayerName(string newName)
        {
            sharedInstance.playerName = newName;
        }

        public string GetPlayerName()
        {
            return sharedInstance.playerName;
        }



    }
}
