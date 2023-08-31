using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeckAndDiscard : MonoBehaviour
{
    PileControl control;
    
    public int maxDeckSize = 30;
    public int heavyAttack, quickAttack, defense, jab, sweep;
    private int tempQuick, tempHeavy, tempDefense, tempJab, tempSweep;
    public int handSize;
    public int currentDeckSize;
    public int discardPile;
    public int discardedHeavyAttack, discardedQuickAttack, discardedDefenseCard, discardedSweepCard, discardedJabCard;
    public int handHA, handQA, handD, handJab, handSweep;
    public int handControl = 0;
    public GameObject cardToHand;
    public GameObject handZone;
    public bool drawControl = true;
    public int cardCurrency, tempCardCurrency;


    private void Start()
    {
        control = GameObject.Find("Pile Control").GetComponent<PileControl>();
        handZone = GameObject.Find("Hand Zone");   
        tempHeavy = heavyAttack;
        tempQuick = quickAttack;
        tempDefense = defense;
        tempCardCurrency = cardCurrency;
        currentDeckSize = heavyAttack + quickAttack + defense;
    }
    private void CardCloner(GameObject CardToHand)
    {
        GameObject clone;
        for (int i = 0; i < 1; i++)
        {
            clone = Instantiate(CardToHand, transform.position, transform.rotation);
            clone.tag = "Card Clone";
        }
    }
    public void CardDraw()
    {
        if (drawControl)
        {
            int drawer = 0;
            while (handControl < handSize)
            {
                drawer = Random.Range(0, 5);
                if (drawer == 0 && heavyAttack != 0)
                {
                    cardToHand = GameObject.Find("HeavyAttackCard");
                    heavyAttack--;
                    handHA ++;
                    CardCloner(cardToHand);
                    handControl += 1;
                }
                if (drawer == 1 && quickAttack != 0)
                {
                    cardToHand = GameObject.Find("QuickAttackCard");
                    quickAttack--;
                    handQA++;
                    CardCloner(cardToHand);
                    handControl += 1;
                }
                if(drawer == 2 && defense != 0)
                {
                    cardToHand = GameObject.Find("DefenseCard");
                    defense--;
                    handD++;
                    CardCloner(cardToHand);
                    handControl += 1;
                }
                if(drawer == 3 && defense != 0)
                {
                    cardToHand = GameObject.Find("JabCard");
                    jab--;
                    handJab++;
                    CardCloner(cardToHand);
                    handControl += 1;
                }
                if (drawer == 4 && defense != 0)
                {
                    cardToHand = GameObject.Find("SweepCard");
                    sweep--;
                    handSweep++;
                    CardCloner(cardToHand);
                    handControl += 1;
                }
                if (quickAttack == 0 && quickAttack == 0)
                {
                    Suffle();
                }
                currentDeckSize = heavyAttack + quickAttack + defense + sweep + jab;
                control.SetPileValues();
            }
        }
        drawControl = false;
    }
    private void Suffle()
    {
        discardPile = 0;
        
        discardedHeavyAttack = 0;
        discardedQuickAttack = 0;
        discardedDefenseCard = 0;
        discardedJabCard = 0;
        discardedSweepCard = 0;
        if (handControl < handSize)
        {
            heavyAttack = tempHeavy;
            quickAttack = tempQuick;
            defense = tempDefense;
            jab = tempJab;
            sweep = tempSweep;
            currentDeckSize = maxDeckSize;
            CardDraw();
        }
        else
        {
            heavyAttack = tempHeavy;
            quickAttack = tempQuick;
            defense = tempDefense;
            jab = tempJab;
            sweep = tempSweep;
            currentDeckSize = maxDeckSize;
        }
    }
}

