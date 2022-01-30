using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public AudioSource feetSource;
    public AudioSource vocalSource;

    [SerializeField]
    private AudioClip[] woodSteps = new AudioClip[5];

    private int previousStep = -1;

    [SerializeField]
    private AudioClip[] tileSteps = new AudioClip[5];

    [SerializeField]
    private AudioClip[] carpetSteps = new AudioClip[5];

    [SerializeField]
    private AudioClip[] barksIdle = new AudioClip[5];

    [SerializeField]
    private AudioClip[] barksNervous = new AudioClip[2];


    [SerializeField]
    private AudioClip kneeshake;

    bool idleIntroPlayed = false;

    void Awake()
    {
        AudioSource[] tempAudioSourceFind;
        tempAudioSourceFind = gameObject.GetComponents<AudioSource>();

        feetSource = tempAudioSourceFind[0];
        vocalSource = tempAudioSourceFind[1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void NewClip (AudioSource source)
    {
        float randomNum =
        source.clip = source[];
    }*/

    public void PlayIdle()
    {
        vocalSource.clip = barksIdle[0];
        int randomNum = 1;


        //FYI all arrays should have an equal length for this to work
        if (!idleIntroPlayed)
        {
            vocalSource.PlayOneShot(barksIdle[0]);
            idleIntroPlayed = true;

            if (kneeshake != null & !feetSource.isPlaying)
            {
                feetSource.clip = kneeshake;
                feetSource.Play();
            }
        }
        else
        {
            while (randomNum == previousStep)
            {
                randomNum = Random.Range(1, barksIdle.Length);
            }

            vocalSource.PlayOneShot(barksIdle[randomNum]);

            
            
            previousStep = randomNum;
        }
        
    }

    
    //Function plays footstep based on what type of material it receives
    public void PlayFootstep(string  material)
    {
        //walking primes the idle intro to be played first
        idleIntroPlayed = false;
        feetSource.Stop();

        int randomNum = 0;

        //FYI careful with changing the ranges of the arrays. Make sure they're the same for footsteps.
        while (randomNum == previousStep)
        {
            randomNum = Random.Range(0, woodSteps.Length);
        }
        

        
        //print(randomNum.ToString());

        switch (material)
        {
            case "wood":
                
                //Debug.Log("wood step:" + randomNum.ToString());
                feetSource.PlayOneShot(woodSteps[randomNum]);
                previousStep = randomNum;
                break;
            case "tile":

                //Debug.Log("Tile step:" + randomNum.ToString());
                feetSource.PlayOneShot(tileSteps[randomNum]);
                previousStep = randomNum;
                break;
            case "carpet":
                
                //Debug.Log("carpet step:" + randomNum.ToString());
                feetSource.PlayOneShot(carpetSteps[randomNum]);
                previousStep = randomNum;
                break;

        }
    }


}
