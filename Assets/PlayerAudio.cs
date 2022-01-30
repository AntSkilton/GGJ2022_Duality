using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerAudio : MonoBehaviour
{
    ThirdPersonController moveScript;

    [SerializeField]
    private AudioClip[] steps = new AudioClip[12];

    [SerializeField]
    private AudioSource source1, source2, audioSourceVocals;

    [SerializeField]
    bool usedAudioSource1 = false;

    public float timer = 0, timerMax = 0.5f;

    //sets the minimum and max pitch adjustment when playing footsteps. careful tho it does affect audio quality quite drastically (might be better with higher sample rates)
    public float pitchmin = -0.125f, pitchMax = 0.125f;

    private int randomNum = 0, previousStep = -1;

    private void Awake()
    {
        moveScript = gameObject.GetComponent<ThirdPersonController>();

        //looks through object for audio sources and then assigns them in order of appearance.
        /*AudioSource[] tempAudioSourceFind;
        tempAudioSourceFind = gameObject.GetComponents<AudioSource>();

        
        source1 = tempAudioSourceFind[0]; //for footsteps
        source2 = tempAudioSourceFind[1]; //for footsteps
        audioSourceVocals = tempAudioSourceFind[2]; //for vocal barks, idles, etc.
        */
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayFootsteps();
    }

    public void PlayFootsteps()
    {
        if (moveScript.isMoving)
        {


            if (timer >= timerMax)
            {


                //FYI careful with changing the ranges of the arrays. Make sure they're the same for footsteps.
                while (randomNum == previousStep)
                {
                    randomNum = Random.Range(0, steps.Length);
                }

                float pitchRandomiser = 1;
                pitchRandomiser += Random.Range(pitchmin, pitchMax);


                //two audio sources allows the samples that trigger to overlap each other. can be extended to 3 for even more sample triggers.
                if (!usedAudioSource1)
                {
                    
                    source1.pitch = pitchRandomiser;
                    source1.PlayOneShot(steps[randomNum]);
                    usedAudioSource1 = true;
                }
                else
                {
                    
                    source2.pitch = pitchRandomiser;
                    source2.clip = steps[randomNum];
                    source2.PlayOneShot(steps[randomNum]);
                    usedAudioSource1 = false;
                }


                previousStep = randomNum;
                timer = 0;
            }

            timer += Time.deltaTime;

        }
        else
        {
            //ensures footstep plays upon input
            timer = timerMax - 0.01f;
        }
    }
}
