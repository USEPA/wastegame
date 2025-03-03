using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Waste : MonoBehaviour
{
    public GameObject[] WasteObjects;
    private List<GameObject> activeWaste;
    public Material[] colors;
    private float spawnTime;
    private float countdown = 7.5f;
    private float floor = 1.5f;
    private float interval = 1f;
    private bool running;
    public List<GameObject> generatedWaste;
    private List<GameObject> randomWaste;
    public Rigidbody example1, example2, example3;
    private decimal pointAdjust = 0;

    //private int numWaste = 3;                     //numWaste is used by stopProduction() to keep track of how many waste objects can still be spawned

    //Potential Game Changing uniques:
    //Radioactive: makes timer go faster
    //contaminated: infects waste and causes infected waste to subtract points

    // Start is called before the first frame update
    void Start()
    {
        activeWaste = new List<GameObject>();       //activeWaste is a copy of WasteObjects that stopProduction() interacts with and will get reset upon starting agian
        foreach (GameObject waste in WasteObjects)
        {
            activeWaste.Add(waste);
        }
        spawnTime = 0f;                             //tracks how long since last item spawned
        running = false;
        generatedWaste = new List<GameObject>();    //empty list to stare normal waste items
        randomWaste = new List<GameObject>();       //empty list to store irregular waste
        generatedWaste.Add(example1.gameObject);
        generatedWaste.Add(example2.gameObject);
        generatedWaste.Add(example3.gameObject);    //add the 3 pre-spawned example wastes to the tracking list
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= countdown)             //if time limit between item spawns has been reached
            {
                GameObject copy = GetWaste();       //get new waste item
                                                    //next the color and size are randomized, with the item's point value scaling at the same rate as the size
                GameObject new_waste = Instantiate(copy, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
                new_waste.GetComponent<MeshRenderer>().material = colors[Random.Range(0, 3)];
                new_waste.GetComponent<Waste_Point>().adjustPoint(pointAdjust);
                int chance = Random.Range(0, 100);
                if (chance < 25)                    //20-40% smaller has a 25% chance
                {
                    decimal rand = decimal.Round((decimal)Random.Range(0.6f, 0.8f),2);
                    new_waste.GetComponent<Transform>().localScale *= (float)rand;
                    new_waste.GetComponent<Waste_Point>().adjustPoint(rand, false);
                }
                else if (chance < 55)               //0-20% smaller has a 30% chance (since chance>=25 if it reached this statement)
                {
                    decimal rand = decimal.Round((decimal)Random.Range(0.8f, 1f), 2);
                    new_waste.GetComponent<Transform>().localScale *= (float)rand;
                    new_waste.GetComponent<Waste_Point>().adjustPoint(rand, false);
                }
                else if (chance >= 95)              //65-130% bigger has a 5% chance
                {
                    decimal rand = decimal.Round((decimal)Random.Range(1.65f, 2.3f), 2);
                    new_waste.GetComponent<Transform>().localScale *= (float)rand;
                    new_waste.GetComponent<Waste_Point>().adjustPoint(rand, false);
                }
                else if (chance >= 85)              //20-65% bigger has a 10% chance (since chance<95 if it reached this statement)
                {
                    decimal rand = decimal.Round((decimal)Random.Range(1.2f, 1.65f), 2);
                    new_waste.GetComponent<Transform>().localScale *= (float)rand;
                    new_waste.GetComponent<Waste_Point>().adjustPoint(rand, false);
                }
                else                                //0-20% larger has a 30% chance (since 55<chance<85 if it reached this statement)
                {
                    decimal rand = decimal.Round((decimal)Random.Range(1f, 1.2f), 2);
                    new_waste.GetComponent<Transform>().localScale *= (float)rand;
                    new_waste.GetComponent<Waste_Point>().adjustPoint(rand, false);
                }
                if (copy != WasteObjects[3])        //add normal waste items to generatedWaste
                {
                    generatedWaste.Add(new_waste);
                }
                else                                //add irregular waste to randomWaste
                {
                    randomWaste.Add(new_waste);
                }
                if (countdown > floor)              //if time limit between item spawns isn't lowest potential value, decrease it by the interval value
                {
                    countdown -= interval;
                    if (countdown < floor)          //but if decreasing the time limit by the interval value makes it too low, set it to the lowest potential value
                    {
                        countdown = floor;
                    }
                }
                spawnTime = 0f;                     //reset item spawn time limit
            }
        }
    }

    public void GetStarted()
    {
        running = true;
    }

    public void adjustExample()                     //if the point card is selected, adjust the example waste objects accordingly
    {
        example1.GetComponent<Waste_Point>().adjustPoint(pointAdjust);
        example2.GetComponent<Waste_Point>().adjustPoint(pointAdjust);
        example3.GetComponent<Waste_Point>().adjustPoint(pointAdjust);
    }

    public void OutOfTime()
    {
        running = false;
    }

    GameObject GetWaste()
    {
        GameObject new_waste;
        int chance = (int)(Random.value * 100);

        //if (numWaste == 3)                  //If using stopProduction(), use this to set smallChance
        //{
        //    smallChance = chance / 32;      //sets each normal waste at 32% and irregular waste at 4%
        //}
        //else if (numWaste == 2)
        //{
        //    smallChance = chance / 48;      //sets remaining 2 normal waste at 48% and irregular waste at 4%
        //}
        //else if (numWaste == 1)
        //{
        //    smallChance = chance / 96;      //sets remaining 1 normal waste at 96% and irregular waste at 4%
        //}
        //else                                //if no normal waste is able to spawn, always generate irregular waste
        //{
        //    smallChance = 0;
        //}
        float smallChance = chance / 32;    //Otherwise smallChance sets each normal waste at 32% and irregular waste at 4%

        if (smallChance < 1)
        {
            new_waste = activeWaste[0]; 
        } else if (smallChance < 2)
        {
            new_waste = activeWaste[1];
        } else if (smallChance < 3)
        {
            new_waste = activeWaste[2];
        } else
        {
            new_waste = activeWaste[3];
        }
        return new_waste;
    }

    public void setStartSpeed(float val)
    {
        countdown = val;
    }
    
    public void setFloorSpeed(float val)
    {
        floor = val;
    }

    public void setChangeRate(float val)
    {
        interval = val;
    }

    public void setPointAdjust(decimal val)
    {
        pointAdjust = val;
    }

    //stopProduction is a method added but not implemented for stopping production of specific waste types under certain conditions
    //to enable it, uncomment all references to this method, as well as the variable numWaste in this class, along with the variable lastRun in the DestroyObject class

    //public void stopProduction(string tag)      //stopProduction() will remove one of the waste items from the spawn chance
    //{
    //    numWaste -= 1;                          //this will alter the actual spawn rates for the remaining items
    //    if (tag == "Waste 1")                   //remove cubes from the activeWaste list
    //    {
    //        activeWaste.Remove(WasteObjects[0]);
    //    }
    //    else if (tag == "Waste 2")              //remove spheres from the activeWaste list
    //    {
    //        activeWaste.Remove(WasteObjects[1]);
    //    }
    //    else if (tag == "Waste 3")              //remove pills from the activeWaste list
    //    {
    //        activeWaste.Remove(WasteObjects[2]);
    //    }
    //    else{                                   //if passed an invalid tag, reset numWaste
    //        numWaste += 1;
    //    }
    //}

    public void ResetGame()
    {
        foreach (GameObject waste in generatedWaste)
        {
            Destroy(waste);                     //destroy all remaining normal waste not in bins
        }
        foreach (GameObject waste in randomWaste)
        {
            Destroy(waste);                     //destroy all irregular waste
        }
        //activeWaste = new List<GameObject>();   //if activeWaste has been altered because of stopProduction, reset it to include all waste objects again
        //foreach (GameObject waste in WasteObjects)
        //{
        //    activeWaste.Add(waste);             
        //}
        generatedWaste = new List<GameObject>();
        randomWaste = new List<GameObject>();   //start new lists for both normal waste and irregular waste
        generatedWaste.Add(example1.gameObject);
        generatedWaste.Add(example2.gameObject);
        generatedWaste.Add(example3.gameObject);//add the new instances of example waste into the new normal waste list
        countdown = 7.5f;
        floor = 1.5f;
        interval = 1f;
        pointAdjust = 0;                        //reset any altered values backto default
    }
}
