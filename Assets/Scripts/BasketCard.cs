using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketCard : MonoBehaviour
{
    [SerializeField]
    private DestroyObject bin1, bin2, bin3;
    [SerializeField]
    private TMPro.TextMeshProUGUI bottomText;
    private string cardText;
    private int chance;

    // Start is called before the first frame update
    private void Start()
    {
        cardText = bottomText.text;         //storing the card template text
        GenerateRandom();                   //initial randomization
    }

    public void RandomizeLimit()            //when this card is selected, apply the randomized values to the waste bins
    {
        bin1.setTimeLimit(chance);
        bin2.setTimeLimit(chance);
        bin3.setTimeLimit(chance);
    }

    private void GenerateRandom()
    {
        chance = Random.Range(10,26);       //random int between 10(inclusive) and 26(exclusive)
        bottomText.text = cardText + chance.ToString() + " seconds";
    }

    public void ResetGame()                 //when reseting the game, randomize again
    {
        GenerateRandom();
    }
}
