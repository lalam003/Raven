using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FallingEvent : EventTask
{
    [SerializeField]
    private float fallRate = 0;

    [SerializeField]
    private Transform fallLoc;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void ExecuteTask()
    {
        gameObject.SetActive(true);
        StartCoroutine(fallingObject());
    }

    private IEnumerator fallingObject()
    {
        yield return null;

        float curr = 0;
        Vector3 start = transform.position;
        Vector3 end = fallLoc.position;

        while(curr < 1)
        {
            curr += (Time.deltaTime * fallRate);
            transform.position = Vector3.Lerp(start, end, curr / 1);
            Debug.Log(curr);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
