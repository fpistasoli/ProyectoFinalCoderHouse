using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Bonfire.Control;

public class PostGlobalController : MonoBehaviour
{

    private PostProcessVolume globalVolume;


    private void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onMistGenerator += statusColorEffect;
        PlayerController.onBossPoint += statusColorEffect;


        //statusColorEffect(false);



    }

    public void statusColorEffect(bool status)
    {
        Debug.Log("ENTRO A STATUS COLOR EFFECT");
        ColorGrading colorFX;
        globalVolume.profile.TryGetSettings(out colorFX);
        Debug.Log(colorFX.name);
        colorFX.active = status;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
