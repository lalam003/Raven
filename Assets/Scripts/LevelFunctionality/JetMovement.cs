using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
    public Rigidbody2D Driver;
    [SerializeField]
    private float rotationSpeed = 5;

    void Update()
    {
        if (Driver.velocity != Vector2.zero)
        {
            float angle = (Mathf.Atan2(-Driver.velocity.y, -Driver.velocity.x) * Mathf.Rad2Deg);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        }
    }
}
