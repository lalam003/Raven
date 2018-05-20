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

    public bool CanMove
    {
        get
        {
            return playerMovement.CanMove;
        }
        set
        {
            playerMovement.CanMove = value;
        }
    }

    [SerializeField]
    private Transform respawn;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        Blackboard.Player = this;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void PlayerDeath()
    {
        gameObject.transform.position = respawn.position;
    }
}
