using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCard : MonoBehaviour
{
    [SerializeField]
    private DestroyObject bin1, bin2, bin3;
    [SerializeField]
    private Create_Waste generator;
    [SerializeField]
    private TMPro.TextMeshProUGUI bottomText;
    private string cardText;
    private decimal[] options;
    private decimal val;

    // Start is called before the first frame update
    void Start()
    {
        cardText = bottomText.text;         //storing the card template text
        GenerateRandom();                   //initial randomization
    }

    public void RandomizePoints()           //when this card is selected, apply the randomized values to the waste bins
    {
        generator.setPointAdjust(val);
    }

    private void GenerateRandom()
    {
        val = decimal.Round((decimal)Random.Range(0.2f, 1f),1);             //random float between 0.2 and 1, rounded to 1 decimal point
        decimal temp = val * 100;                                           //for representing the point multiplier on the card, turn it into a percentile
        bottomText.text = cardText + "+" + temp.ToString("0.") + "% points";
    }

    public void ResetGame()                 //when reseting the game, randomize again
    {
        GenerateRandom();
    }
}
