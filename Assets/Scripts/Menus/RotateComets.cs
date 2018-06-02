using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateComets : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.05f;
	void Update ()
    {
        transform.Rotate(0, 0, speed);
	}
}
