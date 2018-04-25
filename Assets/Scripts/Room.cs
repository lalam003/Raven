using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private Vector3 cameraFocalPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.GetComponent<PlayerMovement>().Equals(null))
        {
            Camera.Instance.MoveCamera(cameraFocalPoint);
        }
    }
}
