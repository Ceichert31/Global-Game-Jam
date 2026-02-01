using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirection : MonoBehaviour
{
    private WanderToPoint wand;
    Tweener flipTween;

    [SerializeField]
    private float flipDuration = 0.3f;

    [SerializeField]
    private Ease flipEase = Ease.Linear;

    [SerializeField]
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        wand = GetComponent<WanderToPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wand.getMove())
        {
            if (wand.getMoveDirection().x < 0)
            {
                flipTween?.Kill(true);
                flipTween = sprite
                    .transform.DOLocalRotate(new Vector3(0, 180, 0), flipDuration)
                    .SetEase(flipEase);
            }
            if (wand.getMoveDirection().x > 0)
            {
                flipTween?.Kill(true);
                flipTween = sprite
                    .transform.DOLocalRotate(new Vector3(0, 0, 0), flipDuration)
                    .SetEase(flipEase);
            }
        }
    }
}
