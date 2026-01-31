using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectLight : MonoBehaviour
{
    [SerializeField]
    GameObject spotlight;
    [SerializeField] private int playerLayer;
    [SerializeField] private int actorLayer;
    [SerializeField] private int numActors = 1;
    [SerializeField] float pointsLost = 4.0f;
    [SerializeField] float pointsGained = 2.0f;

    private void Update()
    {
        // Seeing if things are colliding and where
        Collider2D spotCollider = spotlight.gameObject.GetComponent<Collider2D>();
        ContactFilter2D contacts = new ContactFilter2D();
        contacts.SetLayerMask(Physics2D.GetLayerCollisionMask(spotlight.layer));
        contacts.useLayerMask = true;

        List<Collider2D> collisions = new List<Collider2D>();
        int colCount = spotCollider.OverlapCollider(contacts, collisions);
        int i = 0;
        foreach (Collider2D col2d in collisions)
        {
            i++;
            Debug.Log("HIT" + i + ":" + "(" + col2d.transform.position + ")");
        }

        CheckActors(collisions);
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
