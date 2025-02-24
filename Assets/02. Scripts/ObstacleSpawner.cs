using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // 빈 오브젝트의 위치
    [SerializeField] private GameObject topObstacle;
    [SerializeField] private GameObject bottomObstacle;

    [Header("Debug Handler")]
    [SerializeField] private bool showTopObstacleDebug;
    [SerializeField] private Color showTopColor;
    [Space]
    [SerializeField] private bool showBottomObstacleDebug;
    [SerializeField] private Color showBottomColor;

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
        CreateObstacle();
    }

    public void CreateObstacle()
    {
        if (spawnHandler.CanSpawnObstacle())
            spawnHandler.ObstacleSpawner.Add(RandomSpawnObstacle());
    }

    public ObstacleSpawner RandomSpawnObstacle()
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

        return this;
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 topDrawPosition = pos + new Vector2(0, topSpawnY);
        Vector2 bottomDrawPosition = pos + new Vector2(0, bottomSpawnY);

        Color origin = Gizmos.color;

        if (showTopObstacleDebug)
            DrawObstacleDebug(topDrawPosition, Vector2.down, showTopColor);

        if (showBottomObstacleDebug)
            DrawObstacleDebug(bottomDrawPosition, Vector2.up, showBottomColor);
    }

    private void DrawObstacleDebug(Vector2 startPosition, Vector2 Direction, Color color)
    {
        Color originColor = Gizmos.color;
        Gizmos.color = showTopColor;
        Gizmos.DrawWireCube(startPosition + Direction * 0.5f, Vector2.one);
        Gizmos.color = originColor;
    }
}
