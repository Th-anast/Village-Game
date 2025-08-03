using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject background;
    public GameObject pauseScreen;
    public GameObject controls;
    public CinemachineBrain cinemachineBrain;
    public GameObject saved;
    public static bool onPause, flag;
    float previousTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        previousTimeScale = Time.timeScale;
        onPause = false;
        flag = false;
        saved.SetActive(false);
        background.SetActive(false);
        pauseScreen.SetActive(false);
        controls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!onPause)
            {
                Pause();
                flag = false;
            }
            else
            {
                Unpause();
                saved.SetActive(false);
            }
        }
        if (onPause)
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                // Save Game                
                DataPersistenceManager.Instance.SaveGame();
                saved.SetActive(true);
                Debug.Log("Save Game");
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                pauseScreen.SetActive(false);
                controls.SetActive(true);
                if (flag)
                {
                    pauseScreen.SetActive(true);
                    controls.SetActive(false);
                }
                flag = !flag;
            }
            if (Input.GetKeyUp(KeyCode.Delete))
            {
                Debug.Log("Quit");
                Application.Quit();
            }
        }
    }

    void Pause()
    {
        onPause = true;
        background.SetActive(onPause);
        pauseScreen.SetActive(onPause);        
        cinemachineBrain.enabled = !onPause;
        previousTimeScale = Time.timeScale;
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios) {
            a.Pause();
        }
        Time.timeScale = 0f;
    }

    void Unpause()
    {
        onPause = false;
        background.SetActive(onPause);
        pauseScreen.SetActive(onPause);
        controls.SetActive(onPause);
        cinemachineBrain.enabled = !onPause;        
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios) {
            a.UnPause();
        }
        Time.timeScale = previousTimeScale;        
    }
}
