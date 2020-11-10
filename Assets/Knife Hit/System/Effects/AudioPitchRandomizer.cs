using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPitchRandomizer : MonoBehaviour
{
    public AudioSource audioSource;
public float minRange = 0.9f;
public float maxRange = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.pitch = Random.Range(minRange,maxRange);
        audioSource.PlayOneShot(audioSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
