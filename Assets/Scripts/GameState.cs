using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Create_Waste waste;
    [SerializeField]
    private Canvas startBoard, scoreBoard, timerParent;
    [SerializeField]
    private GameObject card1, card2, card3, mainMenu;
    [SerializeField]
    private DestroyObject bin1, bin2, bin3;
    [SerializeField]
    private bool productionStart, basketStart, pointStart;
    [SerializeField]
    private Image prodImage, baskImage, poinImage;
    public Rigidbody example1, example2, example3;
    private Vector3 cubeStart, sphereStart, pillStart;
    private Color selectedColor, defaultColor;
    // Start is called before the first frame update
    private void Start()
    {
        timer = timerParent.GetComponentInChildren<Timer>();
        scoreBoard.enabled = false;
        timerParent.enabled = false;
        cubeStart = new Vector3(1.5f, 0.5f, 0.7f);
        sphereStart = new Vector3(0f, 0.5f, 0.7f);
        pillStart = new Vector3(-1.5f, 0.5f, 0.7f);
        selectedColor = new Color32(170, 230, 170, 255);
        defaultColor = new Color32(255, 255, 255, 255);
    }

    public void setProduction()
    {
        productionStart = true;
        prodImage.color = selectedColor;
        basketStart = false;
        baskImage.color = defaultColor;
        pointStart = false;
        poinImage.color = defaultColor;
    }

    public void setBasket()
    {
        productionStart = false;
        prodImage.color = defaultColor;
        basketStart = true;
        baskImage.color = selectedColor;
        pointStart = false;
        poinImage.color = defaultColor;
    }

    public void setPoint()
    {
        productionStart = false;
        prodImage.color = defaultColor;
        basketStart = false;
        baskImage.color = defaultColor;
        pointStart = true;
        poinImage.color = selectedColor;
    }

    public void StartGame()
    {
        if (productionStart)
        {
            card1.GetComponent<ProductionCard>().RandomizeRate();
        } else if (basketStart)
        {
            card2.GetComponent<BasketCard>().RandomizeLimit();
        } else if (pointStart)
        {
            card3.GetComponent<PointCard>().RandomizePoints();
            waste.adjustExample();
        }
        scoreBoard.enabled = true;
        startBoard.enabled = false;
        timerParent.enabled = true;
        card1.SetActive(false);
        card2.SetActive(false);
        card3.SetActive(false);
        mainMenu.SetActive(false);
        example1.GetComponent<Rigidbody>().isKinematic = true;
        example1.transform.position = cubeStart;
        example1.transform.rotation = new Quaternion(0, 0, 0, 0);
        example1.GetComponent<Rigidbody>().isKinematic = false;
        example2.GetComponent<Rigidbody>().isKinematic = true;
        example2.transform.position = sphereStart;
        example2.transform.rotation = new Quaternion(0, 0, 0, 0);
        example2.GetComponent<Rigidbody>().isKinematic = false;
        example3.GetComponent<Rigidbody>().isKinematic = true;
        example3.transform.position = pillStart;
        example3.transform.rotation = new Quaternion(0, 0, 0, 0);
        example3.GetComponent<Rigidbody>().isKinematic = false;
        bin1.startGame();
        bin2.startGame();
        bin3.startGame();
        waste.GetStarted();
        timer.StartTimer();
    }

    public void endGame()
    {
        scoreBoard.enabled = false;
        startBoard.enabled = true;
        timerParent.enabled = false;
        mainMenu.SetActive(true);
        card1.SetActive(true);
        card2.SetActive(true);
        card3.SetActive(true);
        productionStart = false;
        prodImage.color = defaultColor;
        basketStart = false;
        baskImage.color = defaultColor;
        pointStart = false;
        poinImage.color = defaultColor;
        productionStart = false;
        basketStart = false;
        pointStart = false;
    }
}
