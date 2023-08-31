using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public Player player;
    public HealthBar healthBar;
    public DeckAndDiscard deckAndDiscard;
    public Combat combatVar;
    public DefenseBar defense;

    public int oppenenthealth;
    public int oppenentCurrentHP;
    public int opponentAttack;
    public int stance;
    public bool passTurnOp = false;
    public int movementToken = 3;
    public int attackToken = 2;
    public float opponentSpeed = 5f;
    private Vector3 temp;// Distance between opponent and player
    private void Start()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        healthBar = GameObject.Find("Player Health Bar").GetComponent<HealthBar>();
        deckAndDiscard = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
        combatVar = GameObject.Find("Combat Control").GetComponent<Combat>();
        oppenenthealth = 100;
        opponentAttack = 2;
        oppenentCurrentHP = oppenenthealth;
    }
    private void Update()
    {
        if (passTurnOp)
        {
            Combat();
        }
    }
    private void Combat()
    {
        temp.x = player.transform.position.x - transform.position.x;

        for (; movementToken > 0; movementToken--)
        {
            stance = Random.Range(0, 5);

            if (temp.x <= -5 && temp.x >= 5)
            {
                OpponentMovement();
            }

            else if(temp.x <= 5 && temp.x >= -5)
            {
                for (; attackToken > 0; attackToken--)
                {
                    switch (stance)
                    {
                        case 0://Mid
                            if (stance == 0 && (player.stanceChanger == 0 || player.stanceChanger == 2))
                            {//Damage dealing
                                combatVar.DefenseControl();
                            }
                            if (stance == 0 && player.stanceChanger == 1)
                            {//No Damage
                                player.playerCurrentHP -= 0;
                                healthBar.SetHealth(player.playerCurrentHP);
                            } 
                            break;
                        case 1://Low
                            if (stance == 1 && player.stanceChanger == 0)
                            {//Damage dealing
                                combatVar.DefenseControl();
                            }
                            if (stance == 1 && player.stanceChanger == 2)
                            {// No Damage
                                player.playerCurrentHP -= 0;
                                healthBar.SetHealth(player.playerCurrentHP);
                            }
                            break;
                        case 2://Mid guard
                            if (stance == 2 && (player.stanceChanger == 0 || player.stanceChanger == 2))
                            {//Damage dealing
                                combatVar.DefenseControl();
                            }
                            if (stance == 2 && player.stanceChanger == 1)
                            {//No Damage
                                player.playerCurrentHP -= 0;
                                healthBar.SetHealth(player.playerCurrentHP);
                            }
                            break;//Low guard
                        case 3:
                            if (stance == 3 && (player.stanceChanger == 0 || player.stanceChanger == 1))
                            {//Damage dealing
                                combatVar.DefenseControl();
                            }
                            if (stance == 3 && player.stanceChanger == 2)
                            {//No Damage
                                player.playerCurrentHP -= 0;
                                healthBar.SetHealth(player.playerCurrentHP);
                            }
                            break;
                        case 4:// High
                            if (stance == 4 && (player.stanceChanger == 0 || player.stanceChanger == 1 || player.stanceChanger == 2))
                            {//Damage dealing
                                combatVar.DefenseControl();
                            }
                            break;
                    }
                }
            }
        }
        combatVar.PassTurn();
    }
    private void OpponentMovement()
    {
        Vector3 axisControl;
        axisControl.x = player.transform.position.x - transform.position.x;

        if (axisControl.x <= 0)
        {
            StartCoroutine(Move(Vector3.left));
        }
        if(axisControl.x > 0)
        {
            StartCoroutine(Move(Vector3.right));
        }
    }
    public IEnumerator Move(Vector3 direc)
    {
        Vector3 originalPosition, targetPosition;
        float elapsedTime = 0, timeToMove = 0.2f;

        direc.x = direc.x * 6;
        originalPosition = transform.position;
        targetPosition = originalPosition + direc;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

    }
}
