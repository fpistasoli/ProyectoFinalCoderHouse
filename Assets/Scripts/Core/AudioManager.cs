using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Core
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager sharedInstance;

        [SerializeField] private AudioClip[] audioClips;

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
    }



}