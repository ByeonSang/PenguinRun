using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private int totalSpawnerCount; // 5

    [SerializeField] private int createSpawnCount; // 3

    private List<Transform> obstacleTrans;
    private int currentCount; // 0
    private void Awake()
    {
        totalSpawnerCount = GetComponentsInChildren<Spawner>().Length;
        obstacleTrans = new List<Transform>();
        obstacleTrans.Capacity = createSpawnCount;
    }

    public bool CanSpawnObstacle()
    {
        // 현재 스폰된 프리펩이 내가 만들려고하는 개수보다 크거나 같으면 생성x
        if (currentCount >= createSpawnCount || totalSpawnerCount <= 0)
            return false;

        // 총 스포너의 개수에서 
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

    public void AddObstacleList(Transform newTrans)
    {
        obstacleTrans.Add(newTrans);
    }

    public void ClearObstacleList()
    {
        obstacleTrans.Clear();
    }
}
