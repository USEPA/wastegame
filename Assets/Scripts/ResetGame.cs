using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    private Canvas display;
    [SerializeField]
    private ProductionCard card1;
    [SerializeField]
    private BasketCard card2;
    [SerializeField]
    private PointCard card3;
    [SerializeField]
    private Points point1, point2, point3;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Teleport transport;
    [SerializeField]
    private Create_Waste generator;
    [SerializeField]
    private DestroyObject bin1, bin2, bin3;
    [SerializeField]
    private GameObject waste1, waste2, waste3, example1, example2, example3;
    private Vector3 pos1, pos2, pos3;
    private Quaternion rot1, rot2, rot3;
    private MeshCollider resetTrigger;
    [SerializeField]
    private TMPro.TextMeshProUGUI pointText;
    [SerializeField]
    private Light lighting;
    [SerializeField]
    private bool hardMode = false;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Canvas>();
        display.enabled = false;                //turn off the game results popup
        resetTrigger = GetComponentInChildren<MeshCollider>();
        resetTrigger.enabled = false;           //disable the reset button
        pos1 = example1.transform.position;
        pos2 = example2.transform.position;
        pos3 = example3.transform.position;
        rot1 = example1.transform.rotation;
        rot2 = example2.transform.rotation;
        rot3 = example3.transform.rotation;     //copy the position and rotation of the initial waste examples
        pointText.text = "";
    }
    
    public void EndGame()
    {
        decimal collectedPoints = decimal.Round(point1.pointTotal + point2.pointTotal + point3.pointTotal,2);
        if (hardMode)                           //if the player turned on hard mode before selecting a card
        {
            decimal adjustedRemaining = 0;      //go through all remaining waste items not currently in a bin and collect their point totals
            foreach (GameObject remaining in generator.generatedWaste)
            {
                adjustedRemaining += remaining.GetComponent<Waste_Point>().getPoint();
            }                                   //this amount will get subtracted from the points from successfully collected waste
            pointText.text = "Collected Waste Points: " + collectedPoints.ToString("0.00") + "\nUncollected Waste: -" + adjustedRemaining.ToString("0.00") + "\nTotal Points: " + decimal.Round(collectedPoints - adjustedRemaining, 2).ToString("0.00");
        } else                                  //otherwise only collect and display the points from successfully collected waste
        {
            pointText.text = "Collected Waste Points: " + collectedPoints.ToString("0.00");
        }
        
        display.enabled = true;                 //turn on the results display
        resetTrigger.enabled = true;            //and reset button
    }

    public void setDifficulty(bool dif)         //when difficulty is toggled, call this to set it
    {
        hardMode = dif;
    }

    public void FullReset()
    {
        display.enabled = false;                //turn the result screen off
        resetTrigger.enabled = false;           //along with the reset button
        card1.ResetGame();
        card2.ResetGame();
        card3.ResetGame();                      //re-randomize each of the selection cards
        transport.ResetGame();                  //teleport the player back to the start room
        timer.ResetGame();                      //reset the timer
        //next recreate the example waste objects and pass them to the generator to store
        example1 = Instantiate(waste1, pos1, rot1);
        example2 = Instantiate(waste2, pos2, rot2);
        example3 = Instantiate(waste3, pos3, rot3);
        generator.example1 = example1.GetComponent<Rigidbody>();
        generator.example2 = example2.GetComponent<Rigidbody>();
        generator.example3 = example3.GetComponent<Rigidbody>();
        generator.ResetGame();                  //now have the generator delete all waste still on the floor and reset all potentially altered values
        bin1.ResetGame();
        bin2.ResetGame();
        bin3.ResetGame();                       //have the bins delete waste still inside of them and reset all potentially altered values
        point1.ResetGame();
        point2.ResetGame();
        point3.ResetGame();                     //reset the point values for each bin
        lighting.color = Color.white;           //set the room lighting back to normal
    }
}
