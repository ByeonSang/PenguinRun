using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int numBgCount = 2; // ���ȭ�� ������Ʈ��ŭ ����
    public int obstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;

    private ItemSpawner[] itemSpawners;

    public List<ComboChecker> comboCheckers = new List<ComboChecker>();
    public List<GameObject> obstacles = new List<GameObject>();

    public float bgSpeed;
    public float bgTime = 0f;
    BoxCollider2D collider;
    public bool isFirst;
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        itemSpawners = GetComponentsInChildren<ItemSpawner>();
    }
    public void Update()
    {

        if ((bgTime >= 10)) // bgTime�� 5�̻��̸�
        {
            bgSpeed += 0.25f; // bground speed 0.5  ���
            bgTime = 0;
        }

        transform.position += Vector3.left * (bgSpeed + GameManager.Instance.PlusSpeed) * Time.deltaTime;
        bgTime += 1f * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Looper"))
        {
            if (isFirst)
            {
                isFirst = false;
                Destroy(gameObject);
                return;

            }
            float widthOfBgObject = collider.size.x; // ���ȭ�� ���� ����
            Vector3 pos = collider.transform.position; // ���� ��ġ
            pos.x += widthOfBgObject * numBgCount; // ���α��� ��ŭ �̵�
            collider.transform.position = pos; // pos �̵��� ��ġ�� collision ��ġ�� ����( �ݿ�)
            ResetItem();
            ResetObstacles();
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Character>().currentLevel = this;
        }

    }

    public void ResetItem()
    {
        foreach (ItemSpawner spawner in itemSpawners)
        {
            spawner.SetItem();
        }
    }

    public void ResetObstacles()
    {
        //리스트에 담아뒀던 콤보체커들의 콜라이더를 다시 활성화 시키고 리스트를 비워줌
        foreach (ComboChecker combo in comboCheckers)
        {
            combo.col.enabled = true;
        }
        comboCheckers.Clear();
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }
        obstacles.Clear();
    }

}
