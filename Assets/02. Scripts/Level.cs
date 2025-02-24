using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int numBgCount = 1; // 배경화면 오브젝트만큼 생성
    public int obstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;

    public float bgSpeed = 1f;
    public float bgTime =0f;
    public void Update()
    {

        if ((bgTime >= 5)) // bgTime이 5이상이면
        {
            bgSpeed += 0.5f; // bground speed 0.5  상승
            bgTime = 0;
        }
        
        transform.position += Vector3.right * bgSpeed * Time.deltaTime;
        bgTime += 1f *Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Background"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // 배경화면 가로 길이
            Vector3 pos = collision.transform.position; // 현재 위치
            pos.x += widthOfBgObject * numBgCount; // 가로길이 만큼 이동
            collision.transform.position = pos; // pos 이동한 위치를 collision 위치로 삽입( 반영)
            return;
        }

    }

    public void Spawner()
    {
        GameObject[] TopObstacle;
        GameObject[] BottomObstacle;
    }
}
 