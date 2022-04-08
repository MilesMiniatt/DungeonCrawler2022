using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public float blockSize;

    private void Start()
    {
        TextLevelLoader.instance.LoadLevel("Level 1");
    }

    public void MoveForward()
    {
        player.GetComponent<PlayerScript>().MoveForward();
    }

    public void TurnLeft()
    {
        player.GetComponent<PlayerScript>().TurnLeft();
    }

    public void TurnRight()
    {
        player.GetComponent<PlayerScript>().TurnRight();
    }

    public void TurnDown()
    {
        player.GetComponent<PlayerScript>().TurnDown();
    }

    public void TurnUp()
    {
        player.GetComponent<PlayerScript>().TurnUp();
    }
}
