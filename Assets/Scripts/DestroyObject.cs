using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public string object_type;
    public Points pointBoard;
    private decimal points = 0;
    private List<Collider> bin;
    private bool empty = true;
    private float totalTime;
    private int timeLimit = 30;
    private bool finish, lastRun;
    [SerializeField]
    private Create_Waste gen;
    [SerializeField]
    private Timer timer;

    private void Start()
    {
        bin = new List<Collider>();
        totalTime = 0;
        finish = false;
        lastRun = false;
    }

    private void Update()
    {
        pointBoard.pointTotal = decimal.Round(points,2);
        if (!finish)
        {
            if (!empty)
            {
                if (totalTime < timeLimit)
                {
                    totalTime += Time.deltaTime;
                }
                else
                {
                    EmptyBin();
                }
            } if (!lastRun && !timer.timeLeft(timeLimit+1))
            {
                empty = false;
                lastRun = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == object_type && !finish)
        {
            bin.Add(other);
            // //create global point tracker
            empty = false;
            if (timer.timeLeft(timeLimit) && !timer.timeLeft(timeLimit*2))
            {
                lastRun = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == object_type && bin.Contains(other) && !finish)
        {
            bin.Remove(other);
            //if (bin.Count == 0)
            //{
            //    empty = true;
            //}
        }
    }

    private void EmptyBin()
    {
        foreach (Collider waste in bin)
        {
            points += waste.gameObject.GetComponent<Waste_Point>().getPoint();
            gen.generatedWaste.Remove(waste.gameObject);
            Destroy(waste.gameObject);
        }
        bin = new List<Collider>();
        empty = true;
        totalTime = 0;
        //if (lastRun)
        //{
        //    gen.stopProduction(object_type);
        //}
    }

    public void setTimeLimit(int val)
    {
        timeLimit = val;
    }

    public void OutOfTime()
    {
        finish = true;
        foreach (Collider waste in bin)
        {
            //points += waste.gameObject.GetComponent<Waste_Point>().getPoint() * (decimal)(totalTime / timeLimit);
            gen.generatedWaste.Remove(waste.gameObject);
        }
    }

    public void ResetGame()
    {
        foreach (Collider waste in bin)
        {
            Destroy(waste.gameObject);
        }
        bin = new List<Collider>();
        totalTime = 0;
        timeLimit = 30;
        points = 0;
        finish = false;
        empty = true;
        lastRun = false;
    }
}
