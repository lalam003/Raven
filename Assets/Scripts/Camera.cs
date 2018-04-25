using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : Singleton<Camera>
{
    [SerializeField]
    private float cameraTransitionSpeed = 1.0f;

    private IEnumerator currentRoutine;
    
    public bool MoveCamera(Vector3 position)
    {
        if(currentRoutine != null && !currentRoutine.MoveNext())
        {
            currentRoutine = move(position);
            StartCoroutine(currentRoutine);

            return true;
        }

        return false;
    }

    IEnumerator move(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        Vector3 currentPos = startPos;
        float curr = 0.0f;

        yield return null;
        while(currentPos != endPos)
        {
            curr += Time.deltaTime * cameraTransitionSpeed;
            currentPos = Vector3.Lerp(startPos, endPos, curr / 1.0f);
            transform.position = currentPos;
            yield return null;
        }
    }
}
