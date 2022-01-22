using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesTrans : MonoBehaviour {
    public Material MaterialToChange;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeTransperancyButtonClicked()
    {
        if(MaterialToChange.GetFloat("_Cutoff") == 0.0f)
        {
            MaterialToChange.SetFloat("_Cutoff", 0.9f);
        }
        else MaterialToChange.SetFloat("_Cutoff", 0.0f);

    }
}
