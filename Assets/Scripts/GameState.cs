using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Create_Waste waste;
    [SerializeField]
    private Canvas startBoard, scoreBoard, timerParent;
    [SerializeField]
    private GameObject card1, card2, card3, difficulty;
    [SerializeField]
    private DestroyObject bin1, bin2, bin3;
    public Rigidbody example1, example2, example3;
    private Vector3 cubeStart, sphereStart, pillStart;
    // Start is called before the first frame update
    private void Start()
    {
        timer = timerParent.GetComponentInChildren<Timer>();
        scoreBoard.enabled = false;
        timerParent.enabled = false;
        cubeStart = new Vector3(1.5f, 0.5f, 0.7f);
        sphereStart = new Vector3(0f, 0.5f, 0.7f);
        pillStart = new Vector3(-1.5f, 0.5f, 0.7f);
    }


    public void StartGame()
    {
        scoreBoard.enabled = true;
        startBoard.enabled = false;
        timerParent.enabled = true;
        card1.SetActive(false);
        card2.SetActive(false);
        card3.SetActive(false);
        difficulty.SetActive(false);
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
        difficulty.SetActive(true);
        card1.SetActive(true);
        card2.SetActive(true);
        card3.SetActive(true);
    }
}
