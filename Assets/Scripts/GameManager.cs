using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    PlayerScript player;

    [SerializeField]
    Animator cameraAnimator;

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
        player.Initialize();
    }

    public float blockSize;

    private void Start()
    {
    }

    public void MoveForward()
    {
        player.UpdateBlocks();
        if (player.objectFacing.GetComponent<BlockScript>().hasPassed)
        {
            CameraShake();
            return;
        }

        player.MoveForward();
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
