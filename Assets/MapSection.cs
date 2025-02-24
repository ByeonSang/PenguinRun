using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSection : MonoBehaviour
{
    private SpawnHandler spawnHandler;

    private void Awake()
    {
        spawnHandler = GetComponentInChildren<SpawnHandler>();
    }

    public void SetPosition(Vector2 movePosition)
    {
        transform.position = movePosition;
        spawnHandler.ResetObstacleList();
    }
}
