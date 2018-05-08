using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Collections;

public class SceneLoader
{
    public static IEnumerator LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName) || string.IsNullOrWhiteSpace(sceneName))
        {
            yield return null;
        }
        AsyncOperation operation = null;
        try
        {
            operation = SceneManager
                .LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        if (operation != null)
        {
            while (!operation.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            yield return null;
        }
    }

    public static IEnumerator LoadAllScenes(IEnumerable<string> scenes)
    {
        foreach (string name in scenes)
        {
            yield return LoadScene(name);
        }
    }
}