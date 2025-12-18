using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
}
