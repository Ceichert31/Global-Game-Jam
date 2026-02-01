using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudienceMember : MonoBehaviour
{
    //random timer things
    float shakeTimerMax = 3.0f;
    float shakeTimerMin = 0.5f;
    float shakeTimer;
    float currTimer;
    float shakeDuration = 0.5f;
    float shakePower = 0.3f;

    void Start()
    {
        SetTimer();
    }

    void Update()
    {
        currTimer -= Time.deltaTime;

        if (currTimer <= 0)
        {
            Shake();
            SetTimer();
        }
    }

    void SetTimer()
    {
        shakeTimer = Random.Range(shakeTimerMin, shakeTimerMax);
        currTimer = shakeTimer;
    }

    void Shake()
    {
        gameObject.transform.DOJump(gameObject.transform.position, shakePower, 1, shakeDuration);
    }
}
