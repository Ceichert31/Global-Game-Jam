using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicEffects : MonoBehaviour
{
    public Transform comic1;
    public Transform endPos1;
    public float wait1;
    public float wait2;
    public float wait3;
    public float wait4;

    public Transform comic2;
    public Transform endPos2;
    public Transform comic3;
    public Transform endPos3;

    public Ease ease2;
    public float shakeStrength = 0.5f;

    private SpriteRenderer render1;
    private SpriteRenderer render2;
    private SpriteRenderer render3;

    private void Start()
    {
        render1 = comic1.GetComponent<SpriteRenderer>();
        render2 = comic2.GetComponent<SpriteRenderer>();
        render3 = comic3.GetComponent<SpriteRenderer>();

        StartCoroutine(ComicSequence());
    }

    private IEnumerator ComicSequence()
    {
        yield return new WaitForSeconds(wait1);

        render1.DOFade(1, 0.3f);

        comic1
            .DOScale(0.5f, 0.3f)
            .OnComplete(() =>
            {
                comic1.DOShakePosition(0.1f, shakeStrength);
            });

        yield return new WaitForSeconds(wait1);
        render2.DOFade(1, 0.3f);

        comic2
            .DOScale(0.5f, 0.3f)
            .OnComplete(() =>
            {
                comic2.DOShakePosition(0.1f, shakeStrength);
            });
        yield return new WaitForSeconds(wait1);
        render3.DOFade(1, 0.3f);

        comic3
            .DOScale(0.5f, 0.3f)
            .OnComplete(() =>
            {
                comic3.DOShakePosition(0.1f, shakeStrength);
            });
        yield return new WaitForSeconds(wait1);
        SceneManager.LoadSceneAsync("MainGameScene");
    }
}
