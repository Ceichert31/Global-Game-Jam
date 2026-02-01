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

    //particles
    [SerializeField]
    List<ParticleSystem> happyEffects;

    [SerializeField]
    List<ParticleSystem> madEffects;
    float effectTimerMax = 5.0f;
    float effectTimerMin = 2.0f;
    float effectTimer;
    float currEffectTimer;

    [SerializeField]
    private float comboTime = 7.5f;

    [SerializeField]
    private float comboTimer;

    [SerializeField]
    private AudioPitcherSO comboYaySound;

    [SerializeField]
    private AudioPitcherSO comboLostSound;

    private AudioSource source;

    public bool inCombo;

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

        source = GetComponent<AudioSource>();

        tweenTimer = Time.time + TickUpdate;
        SetParticleTimer();
    }

    void Update()
    {
        UpdateSatisfaction();
        UpdateParticleTimer(Time.deltaTime);

        comboTimer += Time.deltaTime;

        if (comboTimer > comboTime && !inCombo)
        {
            inCombo = true;
            comboYaySound.Play(source);
        }
    }

    void UpdateSatisfaction()
    {
        meterFillPercent = currSatisfaction / maxSatisfaction;
        audienceMeter.fillAmount = meterFillPercent;
        //audienceMeter.color = meterGradient.Evaluate(meterFillPercent);

        if (currSatisfaction <= 0)
        {
            //End game
            SceneManager.LoadSceneAsync("GameOverScreen");
        }
    }

    void UpdateParticleTimer(float dt)
    {
        currEffectTimer -= dt;

        if (currEffectTimer <= 0)
        {
            PlayParticleEffect();
            SetParticleTimer();
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

        comboTimer = 0;
        if (inCombo)
        {
            inCombo = false;
            comboLostSound.Play(source);
        }
    }

    void PlayParticleEffect()
    {
        if (currSatisfaction >= 50.0f)
        {
            if (happyEffects.Count == 0)
                return;

            //happy effect
            int index = Random.Range(0, happyEffects.Count);
            happyEffects[index].Play();
        }
        else
        {
            //sad effect
            if (madEffects.Count == 0)
                return;

            int index = Random.Range(0, madEffects.Count);
            madEffects[index].Play();
        }
    }

    void SetParticleTimer()
    {
        effectTimer = Random.Range(effectTimerMin, effectTimerMax);
        currEffectTimer = effectTimer;
    }
}
