using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestTool : EditorWindow
{

    int playerHealth, playerMovementCounter;
    int opponentHealth, opponentDamage;
    float opponentSpeedValue;
    int hevAttackCards, defCards, quickCards, sweepCards, jabCards;
    int handsize;
    int cardCost, cardDamage, cardDefense, cardCurrencey;
    string cardName, cardtype;
    int deckSize;
    int movementRefreshCounter;

    CardActions card;
    DeckAndDiscard deck;
    Player player;
    Opponent opponent;
    HealthBar hpP;
    HealthBar hpO;

    [MenuItem("Tools/Test Tool")]
    public static void Window()
    {
        GetWindow(typeof(TestTool));
    }
    private void OnGUI()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        opponent = GameObject.Find("Opponent").GetComponent<Opponent>();
        hpP = GameObject.Find("Player Health Bar").GetComponent<HealthBar>();
        hpO = GameObject.Find("Opponent Health Bar").GetComponent<HealthBar>();
        deck = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();


        GUILayout.Label("Test Tool", EditorStyles.boldLabel);

        playerHealth = EditorGUILayout.IntField("Player Health Value: ", playerHealth);
        playerMovementCounter = EditorGUILayout.IntField("Player Movement Counter", playerMovementCounter);
        movementRefreshCounter = EditorGUILayout.IntField("Player movement refresh counter", movementRefreshCounter);

        if (GUILayout.Button("Change players attributes"))
        {
            player.playerhealth = playerHealth;
            hpP.SetMaxHealth(playerHealth);
            player.playerMovement = playerMovementCounter;
            player.movementRefresher = movementRefreshCounter;
        }
        opponentHealth = EditorGUILayout.IntField("Opponent Health Value: ", opponentHealth);
        if (GUILayout.Button("Change Health value for opponent"))
        {
            opponent.oppenenthealth = opponentHealth;
            hpO.SetMaxHealth(opponentHealth);
        }

        opponentDamage = EditorGUILayout.IntField("Opponent damage Value: ", opponentDamage);
        if (GUILayout.Button("Change Attack value for opponent"))
        {
            opponent.opponentAttack = opponentDamage;
        }
        opponentSpeedValue = EditorGUILayout.FloatField("Opponent Speed Value: ", opponentSpeedValue);
        if(GUILayout.Button("Change Spped value for opponent"))
        {
            opponent.opponentSpeed = opponentSpeedValue;
        }

        deckSize = EditorGUILayout.IntField("Number of card in deck: ", deckSize);
        hevAttackCards = EditorGUILayout.IntField("Numbers of heavy attack cards: ", hevAttackCards);
        quickCards = EditorGUILayout.IntField("Numbers of quick attack cards: ", quickCards);
        defCards = EditorGUILayout.IntField("Numbers of defense cards: ", defCards);
        sweepCards = EditorGUILayout.IntField("Number of sweep cards: ", sweepCards);
        jabCards = EditorGUILayout.IntField("Number of jab cards: ", jabCards);
        if(GUILayout.Button("Save deck customizes"))
        {
            deck.maxDeckSize = deckSize;
            if(hevAttackCards + quickCards + defCards + sweepCards + jabCards <= deckSize)
            {
                Debug.Log("Deck succesfully change");
                deck.heavyAttack = hevAttackCards;
                deck.quickAttack = quickCards;
                deck.defense = defCards;
                deck.sweep = sweepCards;
                deck.jab = jabCards;
            }
            else
            {
                Debug.LogError("Control your max decksize");
            }
        }

        handsize = EditorGUILayout.IntField("Hand Size: ", handsize);
        if(GUILayout.Button("Change hand size"))
        {
            deck.handSize = handsize;
        }

        cardtype = EditorGUILayout.TextField("Card Type: ", cardtype);
        cardName = EditorGUILayout.TextField("Card Name: ", cardName);
        cardCost = EditorGUILayout.IntField("Card Cost: ", cardCost);
        cardDamage = EditorGUILayout.IntField("Card Damage: ", cardDamage);
        cardDefense = EditorGUILayout.IntField("Card Defense: ", cardDefense);

        /*
        if(GUILayout.Button("Generate Card"))
        {
            if (cardtype == "Heavy Attack")
            {
                card = GameObject.Find("HeavyAttackCard").GetComponent<CardActions>();
                card.CardAttributeChanger(cardCost, cardDamage, cardName, cardtype, cardDefense);
                Debug.Log("Heavy attack card value is changed");
            }
            if (cardtype == "Quick Attack")
            {
                card = GameObject.Find("QuickAttackCard").GetComponent<CardActions>();
                card.CardAttributeChanger(cardCost, cardDamage, cardName, cardtype, cardDefense);
                Debug.Log("Quick attack card value is changed");
            }
            if(cardtype == "Defense")
            {
                card = GameObject.Find("DefenseCard").GetComponent<CardActions>();
                card.CardAttributeChanger(cardCost, cardDamage, cardName, cardtype, cardDefense);
                Debug.Log("Defense card value is changed");
            }
            if(cardtype == "Sweep")
            {
                card = GameObject.Find("SweepCard").GetComponent<CardActions>();
                card.CardAttributeChanger(cardCost, cardDamage, cardName, cardtype, cardDefense);
                Debug.Log("Sweep card value is changed");
            }
            if (cardtype == "Jab")
            {
                card = GameObject.Find("JabCard").GetComponent<CardActions>();
                card.CardAttributeChanger(cardCost, cardDamage, cardName, cardtype, cardDefense);
                Debug.Log("Jab card value is changed");
            }
        }*/

        cardCurrencey = EditorGUILayout.IntField("Total Card Currency", cardCurrencey);
        if(GUILayout.Button("Change Currency"))
        {
            deck.cardCurrency = cardCurrencey;
            Debug.Log("Card Currency value is changed");

        }
    }
}
