using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PileControl : MonoBehaviour
{

    DeckAndDiscard pileControl;
    Combat combo;

    public Text deckPile;
    public Text discardPile;
    public Text cardCurrencyPile;
    public Text comboCounterPile;

    int deckNumber;
    int discardNumber;
    int cardCurrency;
    int comboCounter;

    public void Start()
    {
        pileControl = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
        combo = GameObject.Find("Combat Control").GetComponent<Combat>();
        pileControl.currentDeckSize = pileControl.maxDeckSize;
        deckNumber = pileControl.currentDeckSize;
        discardNumber = pileControl.discardPile;
        cardCurrency = pileControl.cardCurrency;
        comboCounter = combo.comboCounter;
    }

    public void Update()
    {
        SetPileValues();
    }

    public void SetPileValues()
    {

        string stringDeckNumber = deckNumber.ToString();
        string stringDiscardNumber = discardNumber.ToString();
        string stringCurrencyNumber = cardCurrency.ToString();
        string stringComboCounter = comboCounter.ToString();

        deckNumber = pileControl.currentDeckSize;
        discardNumber = pileControl.discardPile;
        cardCurrency = pileControl.cardCurrency;
        comboCounter = combo.comboCounter;


        deckPile.text = stringDeckNumber;
        discardPile.text = stringDiscardNumber;
        cardCurrencyPile.text = stringCurrencyNumber;
        comboCounterPile.text = stringComboCounter;
    }

}
