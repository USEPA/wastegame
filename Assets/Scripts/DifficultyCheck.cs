using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyCheck : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField]
    private ResetGame reset;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void click()                 //There is probably better ways to connect these, but this ensures reset's dificulty matches the toggle's visual
    {
        toggle.isOn = !toggle.isOn;
        reset.setDifficulty(toggle.isOn);
    }
}
