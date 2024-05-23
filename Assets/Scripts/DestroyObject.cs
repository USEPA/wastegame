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
    private bool finish;
    [SerializeField]
    private Create_Waste gen;
    [SerializeField]
    private Timer timer;

    //private bool lastRun;                         //lastRun is used to call the generator's stopProduction() to stop production of specific waste types

    private void Start()
    {
        bin = new List<Collider>();                 //start the list of items in the bin
        totalTime = timeLimit;                      //tracks how long until the bin clears, starts after a correct item is placed in the bin
        finish = false;                             //tracks if a game has stopped yet
        //lastRun = false;                            //tracks if this is the last possible time the bin can finish collecting waste before the timer runs out
    }

    private void Update()
    {
        pointBoard.pointTotal = decimal.Round(points,2);
        if (!finish)                                //if the game is currently running
        {
            if (!empty)                             //check how much time has passed since the bin stopped being empty
            {
                if (totalTime > 0)                  //either update the collection countdown based on time since last update
                {
                    totalTime -= Time.deltaTime;
                }
                else                                //or empty the bin to collect all correct waste inside it
                {
                    EmptyBin();
                }
            } 
            //if (!lastRun && !timer.timeLeft(timeLimit+1))       //if the bin is currently empty and the amount of time left on the timer is 1 second more than the collection, automatically start the countdown and label it lastRun
            //{
            //    empty = false;
            //    lastRun = true;
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == object_type && !finish)
        {
            bin.Add(other);                         //if a waste item of the correct type is added, keep track of it
            if (empty)                              //if the bin was empty, update so that it is no longer empty and will start the collection countdown
            {
                empty = false;
            }
            //if (timer.timeLeft(timeLimit) && !timer.timeLeft(timeLimit*2))
            //{                                       //if an item enters the bin, and there is only enough time left on the timer to finish collecting once, label this countdown as lastRun
            //    lastRun = true;
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == object_type && bin.Contains(other) && !finish)
        {
            bin.Remove(other);                      //if an item is removed from the bin, stop tracking it
        }
    }

    private void EmptyBin()
    {
        foreach (Collider waste in bin)             //go through each waste item tracked by the bin and delete it, counting the points stored in the item as it gets deleted
        {
            points += waste.gameObject.GetComponent<Waste_Point>().getPoint();
            gen.generatedWaste.Remove(waste.gameObject);
            Destroy(waste.gameObject);
        }
        bin = new List<Collider>();                 //start a new List
        empty = true;                               //mark the bin as empty again
        totalTime = timeLimit;                      //and reset the countdown
        //if (lastRun)                                //if this countdown was the last run, tell the generator to stop making new waste objects of the assocciated type
        //{
        //    gen.stopProduction(object_type);
        //}
    }

    public void setTimeLimit(int val)
    {
        timeLimit = val;
        totalTime = timeLimit;
    }

    public void OutOfTime()
    {
        finish = true;                              //when the timer runs out, mark the bin as finished
        foreach (Collider waste in bin)             //remove the objects that got correctly sorted but not collected from the general list of waste that the generator tracks
        {                                           //this removes the correctly sorted waste from the negative points calculated in hard mode
            gen.generatedWaste.Remove(waste.gameObject);
            //points += waste.gameObject.GetComponent<Waste_Point>().getPoint() * (decimal)(totalTime / timeLimit);
        }                                           //if desired, correctly sorted waste items could also provide a fraction of their points to either the point total or hard mode's negative point's from uncollected waste 
    }

    public void ResetGame()
    {
        foreach (Collider waste in bin)             //delete the items being tracked by the bin
        {
            Destroy(waste.gameObject);
        }
        bin = new List<Collider>();                 //create a new List for tracking items in the bin
        timeLimit = 30;                             //reset the countdown time limit to the default of 30
        totalTime = timeLimit;                      //reseting the current time left to match
        points = 0;                                 //clear out the points
        finish = false;                             //restart the bin so it can start collecting items again
        empty = true;                               //set the bin to empty now that all items in it have been deleted
        //lastRun = false;                            //if tracking lastRun, reset it to off
    }
}
