using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public decimal pointTotal;
    private string baseString;
    TMPro.TextMeshProUGUI script;

    // Start is called before the first frame update
    void Start()
    {
        pointTotal = 0;
        script = GetComponent<TMPro.TextMeshProUGUI>();
        baseString = script.text;                       //get template text for point sign
    }

    // Update is called once per frame
    void Update()
    {
        script.text = baseString + "\n" + pointTotal.ToString("0.00") + " Points";
    }

    public void ResetGame()
    {
        pointTotal = 0;
    }
}
