using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private GameObject itemPrefabs;

    [SerializeField] private float spawnerSizeX = 0;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private void Start()
    {
        startPosition = transform.localPosition;
        endPosition = transform.localPosition + new Vector3(spawnerSizeX, 0, 0);
        SetItem();
    }

    public void SetItem()
    {
        foreach(GameObject item in items)//루프 되기 이전에 아이템들을 먼저 삭제시키고 다시 생성
        {
            Destroy(item);
        }
        items.Clear();

        Vector3 nowPos = startPosition;
        while (nowPos.x <= endPosition.x)
        {
            if (nowPos.x <= endPosition.x)
            {
                items.Add(Instantiate(itemPrefabs,nowPos,Quaternion.identity));
            }
            nowPos.x += 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoCenter = transform.localPosition + new Vector3(spawnerSizeX/2,0f,0);
        Vector3 gizmoSize = new Vector3(spawnerSizeX, 0.5f, 0f);
        Gizmos.DrawWireCube(gizmoCenter, gizmoSize);
        //
    }
}
