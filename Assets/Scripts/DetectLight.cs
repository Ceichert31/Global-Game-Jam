using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    [SerializeField]
    GameObject spotlight;

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
    }

}
