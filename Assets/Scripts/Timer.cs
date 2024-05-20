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
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
            }
            else
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
        lighting.color = Color.red;
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

    public bool timeLeft(int timeLimit)
    {
        return (timeLimit <= totalTime);
    }
}
