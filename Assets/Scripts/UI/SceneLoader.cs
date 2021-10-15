using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public UnityAction CurrentSceneLoaded;
    public UnityAction NextSceneLoaded;

    public void LoadNext()
    {
        NextSceneLoaded?.Invoke();
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void LoadCurrentScene()
    {
        CurrentSceneLoaded?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
