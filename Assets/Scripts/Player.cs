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
        print("i'm awake");
        PlayerData.currentPlayer = new PlayerData();
        Blackboard.Player = this;
        PlayerData.currentPlayer.currentPosition = transform.position;
        PlayerMovement = GetComponent<PlayerMovement>();
        inventory = GetComponent<Inventory>();
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
