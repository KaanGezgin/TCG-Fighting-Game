using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandZone : MonoBehaviour
{
    DeckAndDiscard deckAndDiscard;

    private void Start()
    {
        deckAndDiscard = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
    }

    public void Discard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
            deckAndDiscard.discardedHeavyAttack += deckAndDiscard.handHA;
            deckAndDiscard.handHA = 0;
            deckAndDiscard.discardedQuickAttack += deckAndDiscard.handQA;
            deckAndDiscard.handQA = 0;
            deckAndDiscard.discardedDefenseCard += deckAndDiscard.handD;
            deckAndDiscard.handD = 0;
            deckAndDiscard.discardedJabCard += deckAndDiscard.handJab;
            deckAndDiscard.handJab = 0;
            deckAndDiscard.discardedSweepCard += deckAndDiscard.handSweep;
            deckAndDiscard.handSweep = 0;
        }
        deckAndDiscard.discardPile = deckAndDiscard.discardedHeavyAttack + deckAndDiscard.discardedQuickAttack + deckAndDiscard.discardedDefenseCard + deckAndDiscard.discardedSweepCard + deckAndDiscard.discardedJabCard;
        deckAndDiscard.handControl = 0;
    }
}
