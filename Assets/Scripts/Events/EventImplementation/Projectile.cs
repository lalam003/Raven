using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float shootSpeed, projectileLifeDuration; 
    private Vector3 shootDir;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        shootDir = transform.up;
        Gizmos.DrawRay(transform.position, shootDir);
    }
#endif

    public void Shoot()
    {
        shootDir = transform.up;
        StartCoroutine(shoot());
    }

    private IEnumerator shoot()
    {
        yield return null;
        while(projectileLifeDuration > 0)
        {
            projectileLifeDuration -= Time.deltaTime;
            Vector2 dir = (shootDir * (Time.deltaTime * shootSpeed));
            transform.position += new Vector3(dir.x, dir.y);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Hit");
            Blackboard.Player.Respawn();
        }
    }
}
