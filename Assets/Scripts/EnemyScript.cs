using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    GameObject player;

    public void Initialize()
    {
        transform.position = GameManager.instance.spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
