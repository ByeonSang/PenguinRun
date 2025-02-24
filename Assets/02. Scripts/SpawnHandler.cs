using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private int totalSpawnerCount; // 5

    [SerializeField] private int createSpawnCount; // 3

    public List<ObstacleSpawner> ObstacleSpawner { get; set; }
    private int currentCount; // 0
    private void Awake()
    {
        ResetObstacleList();
        ObstacleSpawner = new List<ObstacleSpawner>();
        ObstacleSpawner.Capacity = createSpawnCount;
    }

    public bool CanSpawnObstacle()
    {
        // ���� ������ �������� ���� ��������ϴ� �������� ũ�ų� ������ ����x
        if (currentCount >= createSpawnCount || totalSpawnerCount <= 0)
            return false;

        // �� �������� �������� 
        if (totalSpawnerCount-- <= createSpawnCount)
        {
            currentCount++;
            return true;
        }

        int Number = Random.Range(0, 2);
        if (Number == 1)
        {
            currentCount++;
            return true;
        }

        return false;
    }

    public void ResetObstacleList()
    {
        totalSpawnerCount = GetComponentsInChildren<ObstacleSpawner>().Length;
        currentCount = 0;

        StartCoroutine(ResetObstacle());
    }

    private IEnumerator ResetObstacle()
    {
        foreach(var obstacle in ObstacleSpawner)
        {
            if(CanSpawnObstacle())
            {
                obstacle.RandomSpawnObstacle();
                yield return null;
            }
        }
    }
}
