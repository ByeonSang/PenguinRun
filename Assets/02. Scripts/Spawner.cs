using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 빈 오브젝트의 위치
    [SerializeField] private GameObject topObstacle;
    [SerializeField] private GameObject bottomObstacle;
    [SerializeField] private GameObject[] items;

    [SerializeField] private bool showTopObstacleDebug;
    [SerializeField] private bool showBottomObstacleDebug;

    [Header("Obstacle Info")]
    [SerializeField] private float topSpawnY;
    [SerializeField] private float bottomSpawnY;

    private SpawnHandler spawnHandler;

    private bool isBottom;

    Vector2 TopSpawnPosition;
    Vector2 BottomSpawnPosition;

    private void Awake()
    {
        Vector2 pos = transform.position;
        TopSpawnPosition = pos + new Vector2(0, topSpawnY);
        BottomSpawnPosition = pos + new Vector2(0, bottomSpawnY);
    }

    private void Start()
    {
        spawnHandler = GetComponentInParent<SpawnHandler>();
        if(spawnHandler.CanSpawnObstacle())
            spawnHandler.AddObstacleList(RandomSpawnObstacle());
    }

    public Transform RandomSpawnObstacle()
    {
        isBottom = (Random.Range(0, 2) == 0);

        GameObject go;
        if (isBottom)
        {
            go = Instantiate(bottomObstacle);
            go.transform.parent = transform;
            go.transform.localPosition += (Vector3)BottomSpawnPosition;
        }
        else
        {
            go = Instantiate(topObstacle);
            go.transform.parent = transform;
            go.transform.localPosition += (Vector3)TopSpawnPosition;
        }

        return go.transform;
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 topDrawPosition = pos + new Vector2(0, topSpawnY);
        Vector2 bottomDrawPosition = pos + new Vector2(0, bottomSpawnY);

        Color origin = Gizmos.color;

        Gizmos.color = Color.green;
        if(showTopObstacleDebug)
        {
            Gizmos.DrawWireCube(topDrawPosition - Vector2.up * 0.5f, Vector2.one);
        }

        if (showBottomObstacleDebug)
        {
            Gizmos.DrawWireCube(bottomDrawPosition + Vector2.up * 0.5f, Vector2.one);
        }

        Gizmos.color = origin;
    }
}
