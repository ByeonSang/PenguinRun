using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private int totalSpawnerCount; // 5

    [SerializeField] private int createSpawnCount; // 3

    public List<Transform> ObstacleTrans { get; set; }
    private int currentCount; // 0
    private void Awake()
    {
        totalSpawnerCount = GetComponentsInChildren<ObstacleSpawner>().Length;
        ObstacleTrans = new List<Transform>();
        ObstacleTrans.Capacity = createSpawnCount;
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
        currentCount = 0;
        foreach (var trans in ObstacleTrans)
        {
            trans.gameObject.SetActive(false);
            trans.gameObject.SetActive(true);
        }
    }
}
