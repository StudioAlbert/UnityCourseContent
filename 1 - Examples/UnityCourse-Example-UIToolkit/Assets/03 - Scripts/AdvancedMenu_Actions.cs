using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once InconsistentNaming
public class AdvancedMenu_Actions : MonoBehaviour
{
    [SerializeField] private string _gameScene;

    public void LoadGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void Settings()
    {
        Debug.Log("Settings");
    }
    public void Credits()
    {
        Debug.Log("Credits");
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
