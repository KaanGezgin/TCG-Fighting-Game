using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActions : MonoBehaviour
{
    Combat combat;
    DeckAndDiscard deckAndDiscard;
    Player player;
    Opponent opponent;
    DefenseBar defenseBar;

    public int cardCost;
    public int cardDamage;
    public string cardName;
    public string cardType;
    public int cardDefense;
    public GameObject handZone;
    public GameObject card;
    private Vector3 temp;// Distance between opponent and player

    public void CardAttributeChanger(int cardCost, int cardDamage, string cardName, string cardType, int cardDefense)
    {
        this.cardCost = cardCost;
        this.cardDamage = cardDamage;
        this.cardName = cardName;
        this.cardType = cardType;
        this.cardDefense = cardDefense;
    }
    private void Start()
    {
        combat = GameObject.Find("Combat Control").GetComponent<Combat>();
        handZone = GameObject.Find("Hand Zone");
        card = GameObject.Find("Cards");
        deckAndDiscard = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
        player = GameObject.Find("Player1").GetComponent<Player>();
        defenseBar = GameObject.Find("Defense Bar").GetComponent<DefenseBar>();
        opponent = GameObject.Find("Opponent").GetComponent<Opponent>();
    }
    private void Update()
    {
        if (this.tag != "Original")
        {
            transform.SetParent(handZone.transform);
            transform.localScale = Vector3.one;
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
            transform.eulerAngles = new Vector3(25, 0, 0);
        }
        if (this.tag == "Original")
        {
            transform.SetParent(card.transform);
        }

    }
    public void CardPlay()
    {
        int currencyControl;
        currencyControl = deckAndDiscard.cardCurrency - this.cardCost;
        temp.x = player.transform.position.x - opponent.transform.position.x;

        combat.PlayerCombat(this.cardType, this.cardDamage);

        if (!(currencyControl < 0))
        {
            if (temp.x <= 5 && temp.x >= -5)// Damage dealing distance control
            {
                if (this.cardType == "Heavy Attack")
                {
                    deckAndDiscard.discardedHeavyAttack++;
                    deckAndDiscard.cardCurrency -= this.cardCost;
                    Destroy(gameObject);
                    deckAndDiscard.handControl--;
                }
                if (this.cardType == "Quick Attack")
                {
                    deckAndDiscard.discardedQuickAttack++;
                    deckAndDiscard.cardCurrency -= this.cardCost;
                    Destroy(gameObject);
                    deckAndDiscard.handControl--;
                }
                if(this.cardType == "Sweep" && player.stanceChanger == 1)
                {
                    deckAndDiscard.discardedSweepCard++;
                    deckAndDiscard.cardCurrency -= this.cardCost;
                    Destroy(gameObject);
                    deckAndDiscard.handControl--;
                }
            }
            if(temp.x == 10 || temp.x == -10)
            {
                if (this.cardType == "Jab")
                {
                    deckAndDiscard.discardedJabCard++;
                    deckAndDiscard.cardCurrency -= this.cardCost;
                    Destroy(gameObject);
                    deckAndDiscard.handControl--;
                }
            }
            if (this.cardType == "Defense" && defenseBar.defenseBoxCounter < 5)
            {
                deckAndDiscard.discardedDefenseCard++;
                deckAndDiscard.cardCurrency -= this.cardCost;
                defenseBar.defenseBoxCounter++;
                Destroy(gameObject);
                deckAndDiscard.handControl--;
            }
            deckAndDiscard.discardPile = deckAndDiscard.discardedHeavyAttack + deckAndDiscard.discardedQuickAttack + deckAndDiscard.discardedDefenseCard + deckAndDiscard.discardedJabCard + deckAndDiscard.discardedSweepCard;
        }
        else
        {
            Debug.Log("Your Currency is not enough");
        }

    }
}