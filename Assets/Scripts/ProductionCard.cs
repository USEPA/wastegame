using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionCard : MonoBehaviour
{
    [SerializeField]
    private Create_Waste generator;
    [SerializeField]
    private TMPro.TextMeshProUGUI bottomText;
    private string cardText;
    private float startRate, intRate, floor;

    // Start is called before the first frame update
    void Start()
    {
        cardText = bottomText.text;         //storing the card template text
        GenerateRandom();                   //initial randomization
    }

    public void RandomizeRate()             //when this card is selected, apply the randomized values to the generator
    {
        generator.setChangeRate(intRate);
        generator.setFloorSpeed(floor);
        generator.setStartSpeed(startRate);
    }

    private void GenerateRandom()
    {
        floor = (float)System.Math.Round(Random.Range(0.5f,1f), 1);         //random float between 0.5 and 1, rounded to 1 decimal point
        intRate = (float)System.Math.Round(Random.Range(1f,2f), 1);         //random float between 1 and 2, rounded to 1 decimal point
        startRate = (float)System.Math.Round((Random.Range(5f,7f)), 1);     //random float between 5 and 7, rounded to 1 decimal point
        bottomText.text = cardText + startRate.ToString() + " sec high, " + floor.ToString() + " sec low";
    }

    public void ResetGame()                 //when reseting the game, randomize again
    {
        GenerateRandom();
    }
}
