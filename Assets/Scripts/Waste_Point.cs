using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste_Point : MonoBehaviour
{
    private decimal point = 1;

    public void adjustPoint(decimal val, bool adding=true)
    {
        if (adding)
        {
            point += val;
        }
        else
        {
            point = decimal.Round(point * val,2);
        }
    }

    public decimal getPoint()
    {
        return point;
    }
}
