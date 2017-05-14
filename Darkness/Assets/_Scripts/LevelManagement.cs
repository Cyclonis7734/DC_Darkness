using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour {

    public void LoadLevel(string strSceneName)
    {
        SceneManager.LoadScene(strSceneName);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit Request Received...");
        Application.Quit();
    }
}
