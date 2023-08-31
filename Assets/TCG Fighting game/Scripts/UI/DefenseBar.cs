using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseBar : MonoBehaviour
{
    public Player player;
    public Opponent opponent;
    public GameObject defenseBox;
    public GameObject defenseBar;
    public int defenseBoxCounter;


    public void DefenseCardCloner()
    {
        GameObject clone;

        if (defenseBoxCounter < 5)
        {
            for (int i = 0; i < 1; i++)
            {
                clone = Instantiate(defenseBox, transform.position, transform.rotation);
                clone.transform.SetParent(defenseBar.transform);
                clone.transform.localScale = Vector3.one;
                clone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                clone.transform.eulerAngles = new Vector3(25, 0, 0);
                //defenseBoxCounter++;
            }
        }
    }
}
