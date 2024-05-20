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
        display.enabled = false;
        resetTrigger = GetComponentInChildren<MeshCollider>();
        resetTrigger.enabled = false;
        pos1 = example1.transform.position;
        pos2 = example2.transform.position;
        pos3 = example3.transform.position;
        rot1 = example1.transform.rotation;
        rot2 = example2.transform.rotation;
        rot3 = example3.transform.rotation;
        pointText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        decimal collectedPoints = decimal.Round(point1.pointTotal + point2.pointTotal + point3.pointTotal,2);
        if (hardMode)
        {
            decimal adjustedRemaining = 0;
            foreach (GameObject remaining in generator.generatedWaste)
            {
                adjustedRemaining += remaining.GetComponent<Waste_Point>().getPoint();
            }
            pointText.text = "Collected Waste Points: " + collectedPoints.ToString("0.00") + "\nUncollected Waste: -" + adjustedRemaining.ToString("0.00") + "\nTotal Points: " + decimal.Round(collectedPoints - adjustedRemaining, 2).ToString("0.00");
        } else
        {
            pointText.text = "Collected Waste Points: " + collectedPoints.ToString("0.00");
        }
        
        display.enabled = true;
        resetTrigger.enabled = true;
    }

    public void setDifficulty(bool dif)
    {
        hardMode = dif;
    }

    public void FullReset()
    {
        display.enabled = false;
        resetTrigger.enabled = false;
        card1.ResetGame();
        card2.ResetGame();
        card3.ResetGame();
        transport.ResetGame();
        timer.ResetGame();
        try { Destroy(example1); } catch { }
        try { Destroy(example2); } catch { }
        try { Destroy(example3); } catch { }
        example1 = Instantiate(waste1, pos1, rot1);
        example1.GetComponent<Rigidbody>().useGravity = false;
        example2 = Instantiate(waste2, pos2, rot2);
        example2.GetComponent<Rigidbody>().useGravity = false;
        example3 = Instantiate(waste3, pos3, rot3);
        example3.GetComponent<Rigidbody>().useGravity = false;
        generator.example1 = example1.GetComponent<Rigidbody>();
        generator.example2 = example2.GetComponent<Rigidbody>();
        generator.example3 = example3.GetComponent<Rigidbody>();
        generator.ResetGame();
        bin1.ResetGame();
        bin2.ResetGame();
        bin3.ResetGame();
        point1.ResetGame();
        point2.ResetGame();
        point3.ResetGame();
        lighting.color = Color.white;
    }
}
