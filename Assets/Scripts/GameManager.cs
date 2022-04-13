using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    PlayerScript player;

    [SerializeField]
    EnemyScript enemy;

    [SerializeField]
    Animator cameraAnimator;

    public GameObject spawnPoint;

    [SerializeField]
    int stepsBeforeAIMoves = 5;

    [SerializeField]
    int currentStepsTaken = 0;

    [SerializeField]
    List<Vector3> playerStepPositions = new List<Vector3>();

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
        TextLevelLoader.instance.LoadLevel("Level 2");
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        player.Initialize();
        enemy.Initialize();
    }

    public float blockSize;

    private void Start()
    {
    }

    void MoveEnemy()
    {
        if (playerStepPositions.Count <= 0) return;

        if (currentStepsTaken <= stepsBeforeAIMoves) return;

        enemy.transform.position = playerStepPositions[0];
        playerStepPositions.RemoveAt(0);
    }

    public void MoveForward()
    {
        player.UpdateBlocks();
        if (player.objectFacing.GetComponent<BlockScript>().hasPassed)
        {
            CameraShake();
            return;
        }
        else if (player.isMoving) return;

        playerStepPositions.Add(player.transform.position);
        currentStepsTaken++;
        player.MoveForward();
        MoveEnemy();
    }

    public void TurnLeft()
    {
        player.UpdateBlocks();
        if (player.IsInElbow(Vector3.left) || player.IsInTightSpace())
        { 
            CameraShake();
            return;
        }
        player.TurnLeft();
    }

    public void TurnRight()
    {
        player.UpdateBlocks();
        if (player.IsInElbow(Vector3.right) || player.IsInTightSpace())
        {
            CameraShake();
            return;
        }
        player.TurnRight();
    }

    public void TurnDown()
    {
        player.UpdateBlocks();
        if (!player.isUnderwater)
        {
            CameraShake();
            return;
        }
        else
            player.TurnDown();
    }

    public void TurnUp()
    {
        player.UpdateBlocks();
        if (!player.isUnderwater)
        {
            CameraShake();
            return;
        }
        else
            player.TurnUp();
    }

    public void CameraShake()
    {
        cameraAnimator.SetTrigger("Shake");
    }
}
