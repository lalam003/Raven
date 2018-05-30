using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEvent : EventTask
{
    [SerializeField]
    List<Projectile> projectiles;

    private void Awake()
    {
        foreach(Projectile proj in projectiles)
        {
            proj.gameObject.SetActive(false);
        }
    }

    public override void ExecuteTask()
    {
        foreach (Projectile proj in projectiles)
        {
            proj.gameObject.SetActive(true);
            proj.Shoot();
        }
    }
}
