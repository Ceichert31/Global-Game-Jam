using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectLight : MonoBehaviour
{
    [SerializeField]
    private int playerLayer;

    [SerializeField]
    private int actorLayer;

    [SerializeField]
    private int propLayer;

    [SerializeField]
    private int numActors = 1;

    [SerializeField]
    float pointsLost = 4.0f;

    [SerializeField]
    float pointsGained = 2.0f;

    [SerializeField]
    private HashSet<GameObject> objectsInLight = new();

    private void Update()
    {
        if (objectsInLight.Count > 0)
        {
            AudienceManager.instance.LoseSatisfaction(pointsLost * Time.deltaTime);
        }
        else
        {
            AudienceManager.instance.GainSatisfaction(pointsGained * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Add object
        if (collision.gameObject.layer == playerLayer)
        {
            objectsInLight.Add(collision.gameObject);
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IProp propData))
        {
            if (propData.GetPropStatus())
            {
                objectsInLight.Add(collision.gameObject);
            }
            else
            {
                objectsInLight.Remove(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Remove object
        if (collision.gameObject.layer == playerLayer)
        {
            objectsInLight.Remove(collision.gameObject);
            return;
        }

        objectsInLight.Remove(collision.gameObject);
    }

    void CheckActors(List<Collider2D> collisions)
    {
        int actorsInLight = 0;
        foreach (Collider2D c in collisions)
        {
            if (c.gameObject.layer == playerLayer)
            {
                AudienceManager.instance.LoseSatisfaction(pointsLost * Time.deltaTime);
                return;
            }
            else if (c.gameObject.layer == actorLayer)
            {
                actorsInLight++;
            }
        }

        if (actorsInLight == numActors)
        {
            AudienceManager.instance.GainSatisfaction(pointsGained * Time.deltaTime);
        }
    }
}
