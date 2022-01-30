using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugFootsteps : MonoBehaviour
{

    RoamBehaviour roamScript;

    [SerializeField]
    private AudioClip[] bugSteps = new AudioClip[6];

    [SerializeField]
    private AudioClip[] bugIdle = new AudioClip[2];

    [SerializeField]
    private AudioSource source1, source2, source3;

    [SerializeField]
    bool usedAudioSource1 = false;

    public float timer, timerMax, pitchRandomiser;

    //sets the minimum and max pitch adjustment when playing footsteps. careful tho it does affect audio quality quite drastically (might be better with higher sample rates)
    public float pitchmin = -0.125f, pitchMax = 0.125f;

    private int randomNum = 0, previousStep = -1;

    private void Awake()
    {
        roamScript = gameObject.GetComponent<RoamBehaviour>();

        //looks through object for audio sources and then assigns them in order of appearance
        AudioSource[] tempAudioSourceFind;
        tempAudioSourceFind = gameObject.GetComponents<AudioSource>();

        //for footsteps
        source1 = tempAudioSourceFind[0]; //for footsteps
        source2 = tempAudioSourceFind[1]; //for footsteps
        source3 = tempAudioSourceFind[2]; //for vocal barks. you can only say one thing at a time so you only need one.

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaySteps();
    }

    public void PlaySteps ()
    {
        if (roamScript.moving)
        {
            source3.Stop();

            if (timer >= timerMax)
            {


                //FYI careful with changing the ranges of the arrays. Make sure they're the same for footsteps.
                while (randomNum == previousStep)
                {
                    randomNum = Random.Range(0, bugSteps.Length);
                }

                float pitchRandomiser = 1;
                pitchRandomiser += Random.Range(pitchmin, pitchMax);


                //two audio sources allows the samples that trigger to overlap each other. can be extended to 3 for even more sample triggers.
                if (!usedAudioSource1)
                {
                    source1.Stop();
                    source1.pitch = pitchRandomiser;
                    source1.clip = bugSteps[randomNum];
                    source1.Play();
                    usedAudioSource1 = true;
                }
                else
                {
                    //source2.Stop();
                    //source2.clip = bugSteps[randomNum];
                    //source2.Play();

                    source2.pitch = pitchRandomiser;
                    source2.PlayOneShot(bugSteps[randomNum]);
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

            

            if (!source3.isPlaying)
            {
                randomNum = Random.Range(0,bugIdle.Length);

                source3.clip = bugIdle[randomNum];
                source3.Play();
            }
        }
    }    
}
