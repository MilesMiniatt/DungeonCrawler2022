using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    string forward, turnLeft, turnRight, turnUp, turnDown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(forward))
        {
            GameManager.instance.MoveForward();
        }else if (Input.GetKeyDown(turnLeft))
        {
            GameManager.instance.TurnLeft();
        }
        else if (Input.GetKeyDown(turnRight))
        {
            GameManager.instance.TurnRight();
        }
        else if (Input.GetKeyDown(turnUp))
        {
            GameManager.instance.TurnUp();
        }
        else if (Input.GetKeyDown(turnDown))
        {
            GameManager.instance.TurnDown();
        }
    }
}
