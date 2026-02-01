using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudienceManager : MonoBehaviour
{
    public static AudienceManager instance;

    //values!
    float maxSatisfaction = 100.0f;
    float currSatisfaction = 75.0f;

    //UI
    [SerializeField]
    Image audienceMeter;

    [SerializeField]
    private RectTransform audienceMeterObj;

    float meterFillPercent;

    private const float TickUpdate = 0.1f;
    private float tweenTimer;

    [SerializeField]
    private Gradient meterGradient;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        tweenTimer = Time.time + TickUpdate;
    }

    void Update()
    {
        UpdateSatisfaction();
    }

    void UpdateSatisfaction()
    {
        meterFillPercent = currSatisfaction / maxSatisfaction;
        audienceMeter.fillAmount = meterFillPercent;
        audienceMeter.color = meterGradient.Evaluate(meterFillPercent);

        if (currSatisfaction <= 0)
        {
            //End game
            SceneManager.LoadSceneAsync("GameOverScreen");
        }
    }

    public float GetAudienceSatisfaction()
    {
        return currSatisfaction;
    }

    public void GainSatisfaction(float s)
    {
        currSatisfaction += s;

        //clamp value at 100
        if (currSatisfaction > 100.0f)
        {
            currSatisfaction = 100.0f;
        }
    }

    [SerializeField]
    private float shakeAmount = 0.5f;

    public void LoseSatisfaction(float s)
    {
        currSatisfaction -= s;

        //clamp value at 0
        if (currSatisfaction < 0.0f)
        {
            currSatisfaction = 0.0f;
        }

        if (tweenTimer < Time.time)
        {
            tweenTimer = Time.time + TickUpdate;
            audienceMeterObj.DOComplete();
            audienceMeterObj.DOShakeRotation(0.1f, shakeAmount);
        }
    }
}
