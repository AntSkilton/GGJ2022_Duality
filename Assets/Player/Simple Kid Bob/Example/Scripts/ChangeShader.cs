using UnityEngine;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class ChangeShader : MonoBehaviour {

	public Material[] material;
	//Renderer rend;
    Renderer[] rend;
    Camera cam;
    int MatValue = 0;
	// Use this for initialization
	void Start ()
    {
        //rend = GetComponent<Renderer> ();

        cam = Camera.main;
        rend = GetComponentsInChildren<Renderer>();
        foreach (Renderer Check in rend)
        {
            Check.enabled = true;
            Check.sharedMaterial = material[0];
        }
    }

    // Update is called once per frame
    public void ChangeShaderButtonClicked()
    {
        /*
        print("Change mat");
        if (MatValue < material.Length - 1
        ) {
            MatValue++;
        }
        else MatValue = 0;
        UpdateObjects();

        //bad fix to avoid toon shader being translucent or whatever
        if (MatValue == material.Length - 1)
        {
            cam.GetComponent<ScreenSpaceAmbientOcclusion>().enabled = false;
            cam.GetComponent<ScreenSpaceAmbientObscurance>().enabled = false;
        }
        else
        {
            cam.GetComponent<ScreenSpaceAmbientOcclusion>().enabled = true;
            cam.GetComponent<ScreenSpaceAmbientObscurance>().enabled = true;
        }
      
    */
    }
    public void UpdateObjects()
    {
        rend = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer Check in rend)
        {
            Check.enabled = true;
            Check.sharedMaterial = material[0];
            Check.sharedMaterial = material[MatValue];
        }
    }
}
