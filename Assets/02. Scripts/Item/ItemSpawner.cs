using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int spawnerType; //0번이면 젤리(스코어)만 생성, 1번이면 아이템 랜덤 생성
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    private GameObject thisPositionItem = null;

    private void Start()
    {
        SetItem();
    }

    public void SetItem()
    {
        if (thisPositionItem == null)
        {
            if (spawnerType == 0)//이 자리에 해당 아이템이 꼭 와야하는 경우에는 list에 그 아이템 하나만 넣고 spawnerType을 0으로 만들면 됨
            {
                GameObject item = Instantiate(items[0], this.transform);
            }
            else
            {
                int randomItemIndex = Random.Range(0, items.Count);
                Debug.Log(randomItemIndex);
                GameObject item = Instantiate(items[randomItemIndex], this.transform);
            }
        }
        else if (thisPositionItem != null)
        {
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoSize = new Vector3(0.5f, 0.5f, 0f);
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}
