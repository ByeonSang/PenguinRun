using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 빈 오브젝트의 위치
    [SerializeField] private GameObject obstacles;
    [SerializeField] private GameObject[] items;

    [SerializeField] private bool showTopObstacleDebug;
    [SerializeField] private bool showBottomObstacleDebug;
    [SerializeField] private bool showTopItemDebug;
    [SerializeField] private bool showBottomItemDebug;

    [Header("Obstacle Info")]
    [SerializeField] private float topSpawnY;
    [SerializeField] private float bottomSpawnY;

    [Space]
    [SerializeField] private float topScaleY;
    [SerializeField] private float bottomScaleY;

    [Header("Item Percentage")]
    [SerializeField][Range(1, 10f)] private int percentage; // 아이템이 나올 확률

    [Header("ItemSpawn Info")]
    [SerializeField] private Color debugTopColor;
    [SerializeField] private Color debugBottomColor;
    [SerializeField] private float topSpawnItemY;
    [SerializeField] private float bottomSpawnItemY;

    SpawnHandler spawnHandler;

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
            RandomSpawnObstacle();
    }

    public void RandomSpawnObstacle()
    {
        isBottom = (Random.Range(0, 2) == 0);
        GameObject go = Instantiate(obstacles);
        go.transform.parent = transform;

        if(isBottom)
        {
            go.transform.localPosition += (Vector3)BottomSpawnPosition;
            go.transform.localScale = new Vector3(transform.localScale.x, bottomScaleY);
            RandomItemCreate(go.transform, BottomSpawnPosition + new Vector2(0, bottomScaleY + bottomSpawnItemY));
        }
        else
        {
            go.transform.localPosition += (Vector3)TopSpawnPosition;
            go.transform.localRotation = Quaternion.Euler(0, 0, 180f);
            go.transform.localScale = new Vector3(transform.localScale.x, topScaleY);
            RandomItemCreate(go.transform, TopSpawnPosition + new Vector2(0, topScaleY + topSpawnItemY));
        }


    }

    private void RandomItemCreate(Transform parant, Vector2 pos)
    {
        if (items.Length <= 0)
            return;

        if(Random.Range(0, 11) <= percentage)
        {
            int index = Random.Range(0, items.Length);
            GameObject item = Instantiate(items[index]);
            item.transform.localPosition = pos;
        }
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
            Vector2 debugPos = topDrawPosition - new Vector2(0, topScaleY / 2) / 2f;
            Gizmos.DrawWireCube(debugPos, new Vector2(1, topScaleY / 2));
        }

        if (showBottomObstacleDebug)
        {
            Vector2 debugPos = bottomDrawPosition + new Vector2(0, bottomScaleY / 2) / 2f;
            Gizmos.DrawWireCube(debugPos, new Vector2(1, bottomScaleY/ 2));
        }

        if(showTopItemDebug)
        {
            Gizmos.color = debugTopColor;
            Gizmos.DrawWireCube(topDrawPosition + new Vector2(0, topScaleY + topSpawnItemY), Vector3.one);
        }

        if(showBottomItemDebug)
        {
            Gizmos.color = debugBottomColor;
            Gizmos.DrawWireCube(bottomDrawPosition + new Vector2(0, bottomScaleY + bottomSpawnItemY), Vector3.one);
        }

        Gizmos.color = origin;
    }
}
