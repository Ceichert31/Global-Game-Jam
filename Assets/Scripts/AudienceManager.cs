using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceManager : MonoBehaviour
{
    public static AudienceManager instance;

    //values!
    float maxSatisfaction = 100.0f;
    float currSatisfaction = 75.0f;

    //UI
    [SerializeField] Image audienceMeter;
    float meterFillPercent;

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
    }

    void Update()
    {
        UpdateSatisfaction();
    }

    void UpdateSatisfaction()
    {
        meterFillPercent = currSatisfaction / maxSatisfaction;
        audienceMeter.fillAmount = meterFillPercent;
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

    public void LoseSatisfaction(float s)
    {
        currSatisfaction -= s;

        //clamp value at 0
        if (currSatisfaction < 0.0f)
        {
            currSatisfaction = 0.0f;
        }
    }
}
