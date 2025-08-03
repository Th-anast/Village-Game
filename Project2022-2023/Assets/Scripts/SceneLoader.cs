using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    void Start() 
    {
        loadingScreen.SetActive(false);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsynchronously(index));
    }

    IEnumerator LoadSceneAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);   
        while(!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
