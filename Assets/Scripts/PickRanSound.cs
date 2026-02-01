using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRanSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private List<AudioClip> clipList;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            AudioClip clip = PickSound();
            source.PlayOneShot(clip);
            timer = clip.length + Time.time;
        }
        
    }

    // Pick a random sound
    private AudioClip PickSound()
    {
        int random = Random.Range(0, clipList.Count);

        return clipList[random];
    }
}
