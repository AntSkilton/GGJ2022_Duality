using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotTime : MonoBehaviour {

    public GameObject Buttons;
    bool Reactivate = false;
    int counter = 0;
    string ScreenshotPath;
    // Use this for initialization
    void Start ()
    {
        Buttons = GameObject.Find("Buttons");

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Reactivate)
        {
            Buttons.SetActive(true);
            Reactivate = false;
        }

        if (Input.GetKeyDown("space"))
        {

            Buttons.SetActive(false);

            ScreenshotPath = Application.dataPath + "/SomeLevel" + counter + ".png";

            print(ScreenshotPath);
            while (System.IO.File.Exists(ScreenshotPath))
            {
                counter++;
                ScreenshotPath = Application.dataPath + "/SomeLevel" + counter + ".png";
            }

            ScreenCapture.CaptureScreenshot("SomeLevel"+ counter + ".png",2);
            print("Screenshot taken");
            Reactivate = true;
        }
    }
}
