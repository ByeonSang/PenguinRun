using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 빈 오브젝트의 위치들
    [SerializeField] private GameObject Obstacle;

    [Header("Obstacle Info")]
    [SerializeField] private float TopSpawnY;
    [SerializeField] private float BottomSpawnY;

    [Space]
    [SerializeField] private float topScaleY;
    [SerializeField] private float bottomScaleY;

    SpawnHandler spawnHandler;

    private bool isBottom;

    Vector2 TopSpawnPosition;
    Vector2 BottomSpawnPosition;

    private void Awake()
    {
        Vector2 pos = transform.position;
        TopSpawnPosition = pos + new Vector2(0, TopSpawnY);
        BottomSpawnPosition = pos + new Vector2(0, BottomSpawnY);
    }

    private void Start()
    {
        spawnHandler = GetComponentInParent<SpawnHandler>();
        if(spawnHandler.CanSpawnObstacle())
            RandomSpawnObstacle();
    }

    public void RandomSpawnObstacle()
    {
        isBottom = (Random.Range(0, 2) == 0);
        GameObject go = Instantiate(Obstacle);
        go.transform.parent = transform;

        if(isBottom)
        {
            go.transform.localPosition += (Vector3)BottomSpawnPosition;
            go.transform.localScale = new Vector3(transform.localScale.x, bottomScaleY);
        }
        else
        {
            go.transform.localPosition += (Vector3)TopSpawnPosition;
            go.transform.localRotation = Quaternion.Euler(0, 0, 180f);
            go.transform.localScale = new Vector3(transform.localScale.x, topScaleY);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 TopDrawPosition = pos + new Vector2(0, TopSpawnY);
        Vector2 BottomDrawPosition = pos + new Vector2(0, BottomSpawnY);

        Color origin = Gizmos.color;
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(TopDrawPosition, Vector3.one);
        Gizmos.DrawWireCube(BottomDrawPosition, Vector3.one);

        Gizmos.color = origin;
    }
}
