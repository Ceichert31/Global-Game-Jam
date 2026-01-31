using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //timer
    [SerializeField] float roundDuration = 60.0f;
    float currTime;
    //UI
    [SerializeField] Image timerDisplay;
    float timerFillPercent;

    public AudioSource source;
    public AudioPitcherSO testSounds;

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

        currTime = roundDuration;
    }

    void Update()
    {
        UpdateTimer(Time.deltaTime);
    }

    void UpdateTimer(float dt)
    {
        currTime -= dt;

        timerFillPercent = currTime / roundDuration;
        timerDisplay.fillAmount = timerFillPercent;
    }

    [ContextMenu("Test Audio")]
    public void TestAudio()
    {
        testSounds.Play(source);
    }
}
