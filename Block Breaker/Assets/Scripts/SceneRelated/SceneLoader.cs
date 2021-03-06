﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1f;

    [SerializeField] AdsManager adsManager;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //transition from last level to Start Menu
            nextSceneIndex = 0;
        }

        StartCoroutine(LoadScene(nextSceneIndex));
    }

    public void LoadSceneAtIndex(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));
    }

    public void LoadLevelSelection()
    {
        StartCoroutine(LoadScene(1));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadScene(int buildIndex)
    {
        transition.SetTrigger("Start");

        //pauses coroutine for specified amount of seconds
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(buildIndex);

        Time.timeScale = 1f;

        adsManager.ShowRegularAd();
    }

    public void LoadGameOverScene()
    {
        int gameOverSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

        StartCoroutine(LoadScene(gameOverSceneIndex));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
