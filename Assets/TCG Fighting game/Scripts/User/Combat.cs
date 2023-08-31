using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public DeckAndDiscard deckAndDiscard;
    public Opponent opponent;
    public Player player;
    public HealthBar healthBar;
    public DefenseBar defenseBar;
    public HandZone handZoneVar;

    public GameObject[] defenseBox;
    public GameObject handZone;
    public GameObject leftBorder;
    public GameObject rightBorder;
    public int stanceControl;
    public int totalDamage;
    public int cardCost;
    public int comboCounter;

    public bool specialForwardControl = false, specialBackControl = false;
    public int forwardCounter = 0, backCounter = 0;

    private int hitCounter = 0;
    public bool punishmentToken = false;
    private Vector3 temp;// Distance between opponent and player
    public bool discard = false;

    public void Start()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        opponent = GameObject.Find("Opponent").GetComponent<Opponent>();
        healthBar = GameObject.Find("Opponent Health Bar").GetComponent<HealthBar>();
        deckAndDiscard = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
        defenseBar = GameObject.Find("Defense Bar").GetComponent<DefenseBar>();
        handZoneVar = GameObject.Find("Hand Zone").GetComponent<HandZone>();

        leftBorder = GameObject.Find("Left Bolder");
        rightBorder = GameObject.Find("Right Bolder");
        handZone = GameObject.Find("Hand Zone");
     
        comboCounter = 0;
    }
    public void PlayerMovement()
    {
        if (player.transform.position.x != leftBorder.transform.position.x || player.transform.position.x != rightBorder.transform.position.x)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                player.playerMovement--;
                if (player.playerMovement != 0)
                {

                    specialBackControl = true;
                    specialForwardControl = false;
                    StartCoroutine(Move(Vector3.left));
                    if (player.transform.position.x == leftBorder.transform.position.x)
                    {
                        player.transform.position = new Vector3(-28, player.transform.position.y, player.transform.position.z);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                player.playerMovement--;
                if (player.playerMovement != 0)
                {
                    specialBackControl = false;
                    specialForwardControl = true;
                    StartCoroutine(Move(Vector3.right));
                    if (player.transform.position.x == rightBorder.transform.position.x)
                    {
                        player.transform.position = new Vector3(28, player.transform.position.y, player.transform.position.z);
                    }
                }
            }
        }
    }
    public IEnumerator Move(Vector3 direc)
    {
        Vector3 originalPosition, targetPosition;
        float elapsedTime = 0, timeToMove = 0.2f;
        direc.x = direc.x * 6;

        originalPosition = player.transform.position;
        targetPosition = originalPosition + direc;

        while(elapsedTime < timeToMove)
        {
            player.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.transform.position = targetPosition;
    }
    public void DefenseControl()
    {
        GameObject punishmentBox;
        if (defenseBar.defenseBoxCounter >= 0)
        {
            Debug.Log("heyoo");

            if (defenseBar.defenseBoxCounter == 5)
            {
                player.playerCurrentHP -= 0;
                healthBar.SetHealth(player.playerCurrentHP);
                hitCounter++;
                punishmentBox = GameObject.FindGameObjectWithTag("Defense");
                Destroy(punishmentBox);
                defenseBar.defenseBoxCounter--;
                punishmentToken = true;
                Debug.Log("Damage prevented and opponent punished");
            }
            if (punishmentToken != true)
            {
                if (defenseBar.defenseBoxCounter == 4 || defenseBar.defenseBoxCounter == 3)
                {
                    player.playerCurrentHP -= 0;
                    healthBar.SetHealth(player.playerCurrentHP);
                    hitCounter++;
                    defenseBar.defenseBoxCounter--;
                    Debug.Log("Git gud");
                }
                if (defenseBar.defenseBoxCounter == 2 || defenseBar.defenseBoxCounter == 1)
                {
                    player.playerCurrentHP -= (opponent.opponentAttack) / 2;
                    healthBar.SetHealth(player.playerCurrentHP);
                    hitCounter++;
                    defenseBar.defenseBoxCounter--;
                    Debug.Log("Chip damage");
                }
                if (defenseBar.defenseBoxCounter == 0)
                {
                    player.playerCurrentHP -= opponent.opponentAttack;
                    healthBar.SetHealth(player.playerCurrentHP);
                    Debug.Log("Full damage");
                }
                if (hitCounter != 0)
                {
                    for (int i = 0; i < hitCounter; i++)
                    {
                        defenseBox = GameObject.FindGameObjectsWithTag("Defense");
                        Destroy(defenseBox[i]);
                    }
                }
                else if (hitCounter == 0)
                {
                    player.playerCurrentHP -= opponent.opponentAttack;
                    healthBar.SetHealth(player.playerCurrentHP);
                }
            }
        }
    }
    public int ComboCounter(string cardType, int totalDamage)
    {
        temp.x = player.transform.position.x - opponent.transform.position.x;

        if (temp.x < 5 && temp.x > -5)
        {
            if (cardType == "Quick Attack" && !(opponent.stance == 1 || opponent.stance == 2 || opponent.stance == 3 ))
            {
                comboCounter++;
            }
            if (cardType == "Heavy Attack")
            {
                if (comboCounter != 0)
                {
                    comboCounter++;
                    totalDamage += comboCounter;
                    comboCounter = 0;
                }
            }
        }
        return totalDamage;
    }
    public void PlayerCombat(string cardType, int cardDamage)
    {
        string cardtype = cardType;
        totalDamage = 0;
        temp.x = player.transform.position.x - opponent.transform.position.x;

        if (cardType == "Defense" && defenseBar.defenseBoxCounter < 5)
        {
            defenseBar.DefenseCardCloner();
        }
        if (cardType == "Heavy Attack")
        {
            totalDamage = cardDamage + ComboCounter(cardType, totalDamage);
        }
        if (cardType == "Quick Attack")
        {
            totalDamage = cardDamage;
            ComboCounter(cardType, totalDamage);
        }
        if (cardType == "Sweep" && player.stanceChanger == 1)
        {
            totalDamage = cardDamage;
        }
        if (cardType == "Jab" && temp.x == 10 || temp.x == -10)
        {
            totalDamage = cardDamage;
            opponent.oppenentCurrentHP -= SpecialMoves(cardtype, temp);
            healthBar.SetHealth(opponent.oppenentCurrentHP);
        }
        stanceControl = player.stanceChanger;
        if (temp.x < 5 && temp.x > -5)
        { 
            if (!(opponent.oppenentCurrentHP <= 0) && !(deckAndDiscard.cardCurrency < 0))
            {
                switch (stanceControl)
                {
                    case 0://Mid
                        if (stanceControl == 0 && (opponent.stance == 0 || opponent.stance == 4))
                        {//Full Damage
                            opponent.oppenentCurrentHP -= SpecialMoves(cardtype, temp);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        if (stanceControl == 0 && (opponent.stance == 1 || opponent.stance == 3))
                        {//No Damage
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        if (stanceControl == 0 && opponent.stance == 2)
                        {//Cheap Damage
                            opponent.oppenentCurrentHP -= (SpecialMoves(cardtype, temp) / 2);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        break;
                    case 1://Low
                        if (stanceControl == 1 && (opponent.stance == 0 || opponent.stance == 1))
                        {//Full Damage
                            opponent.oppenentCurrentHP -= SpecialMoves(cardtype, temp);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        if(stanceControl == 1 && opponent.stance == 2)
                        {//Guard break
                            opponent.oppenentCurrentHP -= SpecialMoves(cardtype, temp);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        if (stanceControl == 1 && opponent.stance == 3)
                        {//Cheap Damage
                            opponent.oppenentCurrentHP -= (SpecialMoves(cardtype, temp) / 2);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        if (stanceControl == 1 && opponent.stance == 4)
                        {//No Damage
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        break;
                    case 2:// High
                        if (stanceControl == 2 && (opponent.stance == 0 || opponent.stance == 1 || opponent.stance == 2 || opponent.stance == 3 || opponent.stance == 4))
                        {// Full Damage
                            opponent.oppenentCurrentHP -= SpecialMoves(cardtype, temp);
                            healthBar.SetHealth(opponent.oppenentCurrentHP);
                        }
                        break;
                }
            }
        }
    }
    public int SpecialMoves(string cardType, Vector3 temp)
    {
        int finalDamage = 0;
        bool specialMove = false;
        //down + forward + heavy attack
        //Fireball
        if (stanceControl == 1 && specialForwardControl == true && cardType == "Heavy Attack")
        {
            Debug.Log("Fireball");
            finalDamage = totalDamage + 50;
            specialMove = true;
        }
        //Forward + Forward + Heavy Attack
        else if (specialForwardControl == true && cardType == "Heavy Attack" && stanceControl == 0)
        {
            Debug.Log("Forward + Forward + Heavy Attack");
            finalDamage = totalDamage + 20;
            specialMove = true;
        }
        if (specialMove == false)
        {
            finalDamage = totalDamage;
        }
        return finalDamage;
    }
    public void PassTurn()
    {
        if (deckAndDiscard.drawControl == false)
        {
            if (player.passTurnPlayer == true)
            {
                player.passTurnPlayer = false;
                opponent.movementToken = 3;
                opponent.attackToken = 2;
                opponent.passTurnOp = true;
                punishmentToken = false;
                handZoneVar.Discard();
            }
            else if (opponent.passTurnOp == true)
            {
                if (punishmentToken == true)
                {
                    opponent.passTurnOp = false;
                    player.passTurnPlayer = true;
                    deckAndDiscard.drawControl = true;
                    deckAndDiscard.cardCurrency = deckAndDiscard.tempCardCurrency;
                    discard = false;
                    player.playerMovement += player.movementRefresher;
                }
                if (player.passTurnPlayer == false && punishmentToken == false)
                {
                    opponent.passTurnOp = false;
                    player.passTurnPlayer = true;
                    deckAndDiscard.drawControl = true;
                    deckAndDiscard.cardCurrency = deckAndDiscard.tempCardCurrency;
                    discard = false;
                    player.playerMovement += player.movementRefresher;
                }
            }
        }
    }
}
