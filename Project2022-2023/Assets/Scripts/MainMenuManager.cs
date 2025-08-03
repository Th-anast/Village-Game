using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }

    public void NewGame()
    {
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        //SceneManager.LoadSceneAsync("SampleScene");
        LoadScene("SampleScene");
    }

    public void Continue()
    {
        DisableMenuButtons();
        //SceneManager.LoadSceneAsync("SampleScene");
        LoadScene("SampleScene");
    }

    public void Quit()
    {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        creditsButton.interactable = false;
        exitButton.interactable = false;
    }

    private void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsynchronously(scene));
    }

    IEnumerator LoadSceneAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
