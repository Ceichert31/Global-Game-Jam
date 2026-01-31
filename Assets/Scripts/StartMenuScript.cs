using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string GameSceneTxt;
    public string StartSceneTxt;

    public GameObject Startstuff;
    public GameObject Creditstuff;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ToGameScene()
   {
        SceneManager.LoadScene(GameSceneTxt);
   }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(StartSceneTxt);
    }

    public void ToCredits()
    {
        Startstuff.SetActive(false);
        Creditstuff.SetActive(true);
    }

    public void backCredits()
    {
        Startstuff.SetActive(true);
        Creditstuff.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
