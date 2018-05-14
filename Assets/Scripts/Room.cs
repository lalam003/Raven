using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private Transform cameraFocalPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Coll");
        if(!collision.gameObject.GetComponent<PlayerMovement>().Equals(null))
        {
            Debug.Log("Move to " + cameraFocalPoint.position);
            Camera.Instance.MoveCamera(cameraFocalPoint.position);
        }
    }
}
