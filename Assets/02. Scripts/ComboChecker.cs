using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    public BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && QuestManager.Instance.isSuccess == false)
        {
            QuestManager.Instance.AddComboCount();
        }
    }
}
