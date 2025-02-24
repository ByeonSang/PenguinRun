using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int numBgCount = 2; // ���ȭ�� ������Ʈ��ŭ ����
    public int obstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;

    public float bgSpeed = 1f;
    public float bgTime =0f;
    BoxCollider2D collider;
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    public void Update()
    {

        if ((bgTime >= 5)) // bgTime�� 5�̻��̸�
        {
            bgSpeed += 0.5f; // bground speed 0.5  ���
            bgTime = 0;
        }
        
        transform.position += Vector3.left * bgSpeed * Time.deltaTime;
        bgTime += 1f *Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.CompareTag("Looper"))
        {
           
            float widthOfBgObject = collider.size.x; // ���ȭ�� ���� ����
            Vector3 pos = collider.transform.position; // ���� ��ġ
            pos.x += widthOfBgObject * numBgCount; // ���α��� ��ŭ �̵�
            collider.transform.position = pos; // pos �̵��� ��ġ�� collision ��ġ�� ����( �ݿ�)
            return;
        }

    }

  
}
 