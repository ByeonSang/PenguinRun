using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int numBgCount = 1; // ���ȭ�� ������Ʈ��ŭ ����
    public int obstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // ���ȭ�� ���� ����
            Vector3 pos = collision.transform.position; // ���� ��ġ
            pos.x += widthOfBgObject * numBgCount; // ���α��� ��ŭ �̵�
            collision.transform.position = pos; // pos �̵��� ��ġ�� collision ��ġ�� ����( �ݿ�)
            return;
        }

    }
}
 