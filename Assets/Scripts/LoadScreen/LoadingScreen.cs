using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private void Start() {
        string sceneToLoad = SceneLoader.nextScene;

        StartCoroutine(this.MakeTheLoad(sceneToLoad));
    }

    IEnumerator MakeTheLoad(string scene) {
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while(operation.isDone == false) {
            yield return null;
        }
    }
}
