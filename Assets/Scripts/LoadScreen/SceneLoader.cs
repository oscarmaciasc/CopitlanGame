using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string nextScene;

    public static void LoadScene(string name) {
        nextScene = name;
        
        if(InGame.instance != null) {
            InGame.instance.SaveTime();
        }
        
        SceneManager.LoadScene("LoadingScreen");
    }
}
