using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory), typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private Inventory inventory;
    public Inventory Inventory
    {
        get
        {
            return inventory;
        }
    }

    [SerializeField]
    private Transform respawn;

    public PlayerMovement PlayerMovement;

    private void Awake()
    {
        Blackboard.Player = this;
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    public void PlayerDeath()
    {
        StartCoroutine(Camera.Instance.fade());
    }

    public void Respawn()
    {
        gameObject.transform.position = respawn.position;
    }
}
