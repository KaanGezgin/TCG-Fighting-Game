using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    Player player;
    Opponent opponent;
    Combat combatVar;
    DeckAndDiscard deckAndDiscardController;
    public GameObject opponentStanceColor;
    public GameObject playerStanceColor;
    private int stance;
    public void Start()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        opponent = GameObject.Find("Opponent").GetComponent<Opponent>();
        deckAndDiscardController = GameObject.Find("Deck Manager").GetComponent<DeckAndDiscard>();
        combatVar = GameObject.Find("Combat Control").GetComponent<Combat>();
    }
    public void drawAction()
    {
        deckAndDiscardController.CardDraw();
    }
    public void SetPass()
    {
        combatVar.PassTurn();
    }
    public void PlayerStandingStance()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        stance = player.stanceChanger = 0;
        playerStanceColor = GameObject.Find("StandingP");
        playerStanceColor.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
    }
    public void PlayerCrouchingStance()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        stance = player.stanceChanger = 1;
        playerStanceColor = GameObject.Find("CrouchingP");
        playerStanceColor.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
    }
    public void PlayerHighStance()
    {
        player = GameObject.Find("Player1").GetComponent<Player>();
        stance = player.stanceChanger = 2;
        playerStanceColor = GameObject.Find("HighP");
        playerStanceColor.GetComponent<Image>().color = new Color32(255, 0, 0, 225);
    }
}

