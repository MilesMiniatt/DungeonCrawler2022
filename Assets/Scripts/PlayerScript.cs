using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float movementTime = 2f;
    [SerializeField]
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Turn(Vector3 angle)
    {
        if (isMoving) yield return null;

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 finalRotation = currentRotation + angle;

        float timer = 0f;

        Debug.Log("Turning by " + angle);

        isMoving = true;
        while (timer < movementTime)
        {
            timer += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(currentRotation, finalRotation, timer / movementTime);
            yield return null;
        }
        isMoving = false;
    }

    public void TurnLeft()
    {
        if (isMoving) return;

        Vector3 rotation = new Vector3(0, -90, 0);
        StartCoroutine(Turn(rotation));
    }

    public void TurnRight()
    {
        if (isMoving) return;

        Vector3 rotation = new Vector3(0, 90, 0);
        StartCoroutine(Turn(rotation));
    }


    public void TurnUp()
    {
        if (isMoving) return;

        Vector3 rotation = new Vector3(90, 0, 0);
        StartCoroutine(Turn(rotation));
    }

    public void TurnDown()
    {
        if (isMoving) return;

        Vector3 rotation = new Vector3(-90, 0, 0);
        StartCoroutine(Turn(rotation));
    }


    public void MoveForward()
    {
        if (isMoving) return;

        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        Vector3 playerPosition = transform.position;
        Vector3 targetPosition = playerPosition + transform.forward * GameManager.instance.blockSize;

        float timer = 0f;

        isMoving = true;
        while (timer < movementTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(playerPosition, targetPosition, timer / movementTime);
            yield return null;
        }

        isMoving = false;
    }
}
