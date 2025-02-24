using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int spawnerType; //0���̸� ����(���ھ�)�� ����, 1���̸� ������ ���� ����
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
            if (spawnerType == 0)//�� �ڸ��� �ش� �������� �� �;��ϴ� ��쿡�� list�� �� ������ �ϳ��� �ְ� spawnerType�� 0���� ����� ��
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
