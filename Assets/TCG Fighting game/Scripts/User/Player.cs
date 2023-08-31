using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Combat combat;

    public int playerhealth;
    public int playerAttack;
    public int playerCurrentHP;
    public int stanceChanger = 0;
    public bool passTurnPlayer = true;
    public int playerMovement;
    public int movementRefresher;
    private void Start()
    {
        combat = GameObject.Find("Combat Control").GetComponent<Combat>();
      
        playerCurrentHP = playerhealth;
        passTurnPlayer = true;
    }
    private void Update()
    {
        if (passTurnPlayer)
        {
            PlayerTurn();
        }
    }
    private void PlayerTurn()
    {
        if (playerMovement != 0)
        {
            combat.PlayerMovement();
        }
    }
}
