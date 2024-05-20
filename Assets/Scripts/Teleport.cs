using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Vector3 origin;         //start vector of player
    [SerializeField]
    private Transform player;       //transform holding a vector for the player to be transported when a card is selected

    // Start is called before the first frame update
    private void Start()
    {
        origin = player.position;
    }

    public void moveHere()          //send the player to the waste object room
    {
        player.position = GetComponent<Transform>().position;
    }

    public void ResetGame()         //send the player to the starting room
    {
        player.position = origin;
    }
}
