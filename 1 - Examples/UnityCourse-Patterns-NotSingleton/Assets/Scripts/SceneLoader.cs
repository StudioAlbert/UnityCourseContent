using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private static Scene _lastPlayableScene;
    
    private void LoadBackground()
    {
        Scene bgScene = SceneManager.GetSceneByName("BackgroundScene");
        
        if(!bgScene.isLoaded)
        {
            SceneManager.LoadScene("BackgroundScene");
        }
    }
    
    private IEnumerator UnloadActiveScene()
    {
        if (!_lastPlayableScene.IsValid()) yield return null;
        
        AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(_lastPlayableScene);
        
        if (unloadSceneAsync == null) yield return null;
        
        while (!unloadSceneAsync.isDone)
        {
            Debug.Log("Unload active scene");
            yield return null;
        }
    }
    
    private IEnumerator LoadAsync(string sceneName)
    {
       
        AsyncOperation loadScreen = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        if (loadScreen == null) yield return null;
        _lastPlayableScene = SceneManager.GetSceneByName(sceneName);
        
        while (!loadScreen.isDone)
        {
            Debug.Log($"Loading Scene {sceneName}, {loadScreen.progress}");
            yield return null;
        }


    }
    
    public void LoadScene1()
    {
        LoadBackground();
        //LoadAScene("Scene1");
        StartCoroutine(UnloadActiveScene());
        StartCoroutine(LoadAsync("Scene1"));
    }
    public void LoadScene2() 
    {
        LoadBackground();
        //LoadAScene("Scene2");
        StartCoroutine(UnloadActiveScene());
        StartCoroutine(LoadAsync("Scene2"));
    }

}
