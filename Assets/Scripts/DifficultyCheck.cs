using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyCheck : MonoBehaviour
{
    private bool active;
    [SerializeField] private TMP_Text difficultyText;
    [SerializeField]
    private ResetGame reset;

    // Start is called before the first frame update
    void Start()
    {
        setDifficulty(false);
    }

    public void click()                 //There is probably better ways to connect these, but this ensures reset's dificulty matches the toggle's visual
    {
        if (active)
        {
            active = false;
            difficultyText.text = "Hard Mode Off";
        }
        else
        {
            active = true;
            difficultyText.text = "Hard Mode On";
        }
        reset.setDifficulty(active);
    }

    public void setDifficulty(bool dif)
    {
        active = dif;
        if (active)
        {
            difficultyText.text = "Hard Mode On";
        }
        else
        {
            difficultyText.text = "Hard Mode Off";
        }
    }
}
