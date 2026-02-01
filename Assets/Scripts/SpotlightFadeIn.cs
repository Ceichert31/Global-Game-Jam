using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotlightFadeIn : MonoBehaviour
{
    [SerializeField]
    private float lightFadeInDuration = 1.5f;

    [SerializeField]
    private float endLightIntensity = 1f;

    private Light2D spotLight;

    private void Start()
    {
        spotLight = GetComponent<Light2D>();
        spotLight.intensity = 0f;

        StartCoroutine(FadeInLight());
        transform.DOLocalMoveY(3.8f, lightFadeInDuration);
    }

    private IEnumerator FadeInLight()
    {
        float timeElapsed = 0;

        while (timeElapsed < lightFadeInDuration)
        {
            timeElapsed += Time.deltaTime;

            spotLight.intensity = Mathf.Lerp(
                0,
                endLightIntensity,
                timeElapsed / lightFadeInDuration
            );

            yield return null;
        }
        spotLight.intensity = endLightIntensity;
    }
}
