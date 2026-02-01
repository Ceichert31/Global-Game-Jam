using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButtonLogic : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadSceneAsync("MainGameScene");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
