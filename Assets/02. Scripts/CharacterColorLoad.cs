using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterColorLoad : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color originColor = Color.white;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("PlayerColor"))
        {
            string str = PlayerPrefs.GetString("PlayerColor");
            float[] colorValue = str.Split('/').Select(s => float.Parse(s)).ToArray();
            originColor = new Color(colorValue[0], colorValue[1], colorValue[2], colorValue[3]);
        }
        else
        {
            originColor = Color.white;
        }

        _spriteRenderer.color = originColor;
    }
}
