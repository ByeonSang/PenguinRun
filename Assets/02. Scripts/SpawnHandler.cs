using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private int totalSpawnerCount; // 5

    [SerializeField] private int createSpawnCount; // 3

    private int currentCount; // 0
    private void Awake()
    {
        totalSpawnerCount = GetComponentsInChildren<Spawner>().Length;
    }

    public bool CanSpawnObstacle()
    {
        // ���� ������ �������� ���� ��������ϴ� �������� ũ�ų� ������ ����x
        if (currentCount >= createSpawnCount || totalSpawnerCount <= 0)
            return false;

        // �� �������� �������� 
        if(totalSpawnerCount-- <= createSpawnCount)
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
}
