using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMethods : MonoBehaviour
{
    private void StopAnim()
    {
        gameObject.GetComponent<Animation>().enabled = false;
    }
}
