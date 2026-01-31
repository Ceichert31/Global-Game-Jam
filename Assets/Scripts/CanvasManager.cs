using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int AudienceHappiness;

    public GameObject BGImage;
    public TMPro.TextMeshPro RecapText;

    public Sprite StandingOvation;
    public Sprite WhatAShow;
    public Sprite ChaoticSucess;
    public Sprite CompleteDisaster;
    public Sprite AudienceWalkout;
    void Start()
    {
        SetScores();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void GetScore()
    {
        //get score
        SetScores();
    }

    public void SetScores()
    {
        //I **love** if statements

        if (AudienceHappiness >= 90)
        {
            Debug.Log("Standing Ovation");
            BGImage.GetComponent<Image>().sprite= StandingOvation;
        }

        if (90> AudienceHappiness && 75<=AudienceHappiness)
        {
            Debug.Log("What a show");

            BGImage.GetComponent<Image>().sprite = WhatAShow;
        }

        if (74 > AudienceHappiness && 51 <= AudienceHappiness)
        {
            Debug.Log("Chaotic Sucess");

            BGImage.GetComponent<Image>().sprite = ChaoticSucess;
        }

        if (50 > AudienceHappiness && 26 <= AudienceHappiness)
        {
            Debug.Log("Complete Disaster");

            BGImage.GetComponent<Image>().sprite = CompleteDisaster;
        }

        if (25 >= AudienceHappiness)
        {
            Debug.Log("Audience walkout");

            BGImage.GetComponent<Image>().sprite = AudienceWalkout;
        }

    }
}
