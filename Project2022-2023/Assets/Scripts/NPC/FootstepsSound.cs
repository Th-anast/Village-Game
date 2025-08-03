using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    private AudioSource aud;
    [SerializeField] private AudioClip[] Clip;
    float delay_time = 0f;
    int i = 0;

    async void Start()
    {
        aud = GetComponent<AudioSource>();
        foreach (AudioClip n in Clip)
        {
            await Task.Delay((int)delay_time * 600); //Task.Delay input is in milliseconds
            playaudio(n);
        }
    }

    void Update()
    {
        if (PauseManager.onPause)
        {
            aud.Pause();
        }
        else
        {
            aud.UnPause();
            if (i == 7)
            {
                i = 0;
                Start();
            }
        }
    }

    void playaudio(AudioClip n)
    {
        if (aud != null)
        {
            aud.clip = n;
            aud.Play();
            delay_time = n.length + 1; //1 second is added to cater for the loading delay          
            i++;
        }
    }
}
