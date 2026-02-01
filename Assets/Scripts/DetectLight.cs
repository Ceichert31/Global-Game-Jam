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
    private float noPropTimeBeforeDecay = 3f;

    private float emptySetTimer;

    [SerializeField]
    private HashSet<GameObject> objectsInLight = new();

    private void Start()
    {
        emptySetTimer = noPropTimeBeforeDecay;
    }

    private void Update()
    {
        //Timer for if the set is empty, start losing score
        if (objectsInLight.Count == 0)
        {
            emptySetTimer -= Time.deltaTime;
            if (emptySetTimer <= 0)
            {
                AudienceManager.instance.LoseSatisfaction(pointsLost * Time.deltaTime);
                return;
            }
        }
        else
        {
            //Otherwise reset timer
            emptySetTimer = noPropTimeBeforeDecay;
        }

        if (ShouldDrainMeter())
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

        if (collision.gameObject.layer == propLayer)
        {
            objectsInLight.Add(collision.gameObject);
            return;
        }

        if (collision.gameObject.layer == actorLayer)
        {
            objectsInLight.Add(collision.gameObject);
            return;
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
        if (collision.gameObject.layer == propLayer)
        {
            objectsInLight.Remove(collision.gameObject);
            return;
        }
        if (collision.gameObject.layer == actorLayer)
        {
            objectsInLight.Remove(collision.gameObject);
            return;
        }
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

    private bool ShouldDrainMeter()
    {
        foreach (var obj in objectsInLight)
        {
            if (obj.layer == playerLayer)
                return true;

            if (!obj.TryGetComponent(out IProp propData))
                continue;

            if (propData.GetPropStatus())
            {
                return true;
            }

            if (obj.layer == actorLayer)
            {
                return false;
            }
        }
        return false;
    }
}
