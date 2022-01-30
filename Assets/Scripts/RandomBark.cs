using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBark : MonoBehaviour
{
    public List<AudioClip> barks = new List<AudioClip>();
    

    void Awake() {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = barks[Random.Range(0, barks.Count)];
        audioSource.Play();
    }
}
