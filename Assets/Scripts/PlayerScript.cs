using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float movementTime = 2f;
    [SerializeField]
    bool isMoving = false;
    public bool isUnderwater = false;

    public GameObject objectFacing;

    [SerializeField]
    public GameObject leftBlock, rightBlock, backBlock, frontBlock;

    GameObject cantGoBack;
    
    Vector3 lastRotation;

    public void Initialize()
    {
        UpdateBlocks();
    }

    private void Update()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);

        //RaycastHit raycastHit;
        //if (Physics.Raycast(transform.position, forward, out raycastHit, 50))
        //    objectFacing = raycastHit.transform.gameObject;
    }

    public void UpdateBlocks()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 left = transform.TransformDirection(Vector3.left); 
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 back = transform.TransformDirection(Vector3.back);

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, forward, out raycastHit, 10))
        {
            frontBlock = raycastHit.transform.gameObject;
            objectFacing = frontBlock;
        }
        if (Physics.Raycast(transform.position, left, out raycastHit, 10)) {

            leftBlock = raycastHit.transform.gameObject;
        }
        if (Physics.Raycast(transform.position, right, out raycastHit, 10))
        {
            rightBlock = raycastHit.transform.gameObject;
        }
        if (Physics.Raycast(transform.position, back, out raycastHit, 10))
        {
            backBlock = raycastHit.transform.gameObject;
        }

    }

    public bool IsInTightSpace()
    {
        return !frontBlock.GetComponent<BlockScript>().hasPassed &&
            rightBlock.GetComponent<BlockScript>().hasPassed &&
            leftBlock.GetComponent<BlockScript>().hasPassed &&
            backBlock.GetComponent<BlockScript>().hasPassed;
    }

    public bool IsInElbow(Vector3 angle)
    {
        return (leftBlock.GetComponent<BlockScript>().hasPassed == true &&
            angle == Vector3.left ||
            rightBlock.GetComponent<BlockScript>().hasPassed == true &&
            angle == Vector3.right) &&
            backBlock.GetComponent<BlockScript>().hasPassed == true;
    }

    public IEnumerator Turn(Vector3 angle)
    {
        if (isMoving) yield return null;

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 finalRotation = currentRotation + angle;

        lastRotation = angle;

        float timer = 0f;


        isMoving = true;
        while (timer < movementTime)
        {
            timer += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(currentRotation, finalRotation, timer / movementTime);
            yield return null;
        }
        isMoving = false;

        UpdateBlocks();

        if (frontBlock.GetComponent<BlockScript>().hasPassed)
            StartCoroutine(Turn(lastRotation));
        
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

        UpdateBlocks();
    }

    public IEnumerator Move()
    {
        if(cantGoBack != null)
        {
            cantGoBack.transform.Find("Can't Go Back").gameObject.SetActive(false);
        }
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
        UpdateBlocks();

        if (backBlock != null && backBlock.transform.Find("Can't Go Back") != null)
        {
            cantGoBack = backBlock;
            cantGoBack.transform.Find("Can't Go Back").gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Underwater Block")
        {
            if(!isUnderwater)
                isUnderwater = true;
        }
        else if(other.tag == "Block")
        {
            if(isUnderwater)
                isUnderwater = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BlockScript>() != null)
        {
            if (!other.GetComponent<BlockScript>().hasPassed)
            {
                other.GetComponent<BlockScript>().hasPassed = true;
            }
        }
    }
}
