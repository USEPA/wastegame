using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public DestroyObject binOne, binTwo, binThree;
    public Create_Waste generator;
    private float totalTime = 120;
    private TMPro.TextMeshProUGUI timerText;
    private bool running;
    public ResetGame endScreen;
    [SerializeField]
    private Light lighting;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMPro.TextMeshProUGUI>();
        running = false;                //a card has not been selected yet
    }

    // Update is called once per frame
    void Update()
    {
        if (running)                    //once a card has been selected
        {
            if (totalTime > 0)          //if there is still time left, subtract the amount of time since last update from totalTime
            {
                totalTime -= Time.deltaTime;
            }
            else                        //otherwise stop the timer and end the game
            {
                TimeOver();
                totalTime = 0;
                running = false;
            }
        }
        
        //divide the time by 60
        float minutes = Mathf.FloorToInt(totalTime / 60);

        // returns the remainder
        float seconds = Mathf.FloorToInt(totalTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeOver()
    {
        lighting.color = Color.red;     //a visual cue that the timer has hit 0, in future version an audio cue would be good to add here
        binOne.OutOfTime();
        binTwo.OutOfTime();
        binThree.OutOfTime();
        generator.OutOfTime();
        endScreen.EndGame();
    }

    public void StartTimer()
    {
        running = true;
    }

    public void ResetGame()
    {
        totalTime = 120;
    }

    public bool timeLeft(int timeLimit) //a check if some amount of time is less than the time left on the timer
    {
        return (timeLimit <= totalTime);
    }
}
