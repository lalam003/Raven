using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction = Vector2.up;
    private const string conveyerParentName = "ConveyerParent";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Blackboard.PowerOn)
        {
            Transform walker = collision.GetComponent<Transform>();
            if (walker)
            {
                Rigidbody newParent = new GameObject().AddComponent<Rigidbody>();
                newParent.useGravity = false;
                newParent.gameObject.name = conveyerParentName;
                walker = getOuterParent(walker);
                walker.SetParent(newParent.transform);
                newParent.velocity = direction;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Blackboard.PowerOn)
        {
            Transform walker = collision.GetComponent<Transform>();
            if (walker)
            {
                walker = getSecondToOuterParent(walker);
                if (walker.parent.gameObject.name == conveyerParentName)
                {
                    GameObject parentToKill = walker.parent.gameObject;
                    walker.SetParent(null);
                    Destroy(parentToKill);
                }
            }
        }
    }

    private Transform getOuterParent(Transform t)
    {
        Transform current = t;
        if (t.parent != null)
        {
            current = current.parent;
        }
        return current;
    }

    private Transform getSecondToOuterParent(Transform t)
    {
        Transform current = t;
        if (t.parent.parent != null)
        {
            current = current.parent;
        }
        return current;
    }
}
