using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste_Point : MonoBehaviour
{
    private decimal point = 1;

    public void adjustPoint(decimal val, bool adding=true)
    {
        if (adding)             //by default, add val to point
        {
            point += val;
        }
        else                    //otherwise multiply point by val and round to 2 decimal points
        {
            point = decimal.Round(point * val,2);
        }
    }

    public decimal getPoint()
    {
        return point;
    }
}
