using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    PlayerScript player;

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
        Debug.Log(player.objectFacing.name);
        if (player.objectFacing.GetComponent<BlockScript>().hasPassed)
            return;

        player.MoveForward();
    }

    public void TurnLeft()
    {
        player.TurnLeft();
    }

    public void TurnRight()
    {
        player.TurnRight();
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
