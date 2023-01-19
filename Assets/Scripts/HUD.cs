using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class HUD : MonoBehaviour
{
    public int score;
    public SpriteRenderer spriteRendererButton;
    public Sprite[] sprites;
    public Transform meter;
    public GameObject endscreen;
    private int size = 1;
    private Vector3 scale;

    void Update()
    {
        scale = new Vector3(1, size, 1);
        if ((score >= 5) && (Input.GetKeyDown(KeyCode.Space)))
        {
            score -= 5;
            size = 2 * score;

        }
        if (score >= 5)
        {
            spriteRendererButton.sprite = sprites[0];
            //spriteRendererButton.Sprite = Post_Button__0;
        }
        else
        {
            spriteRendererButton.sprite = sprites[1];
        }

        if (size <= 11)
        {
            // Debug.Log(size.ToString() + "meter scale");
            meter.localScale = scale;
        }
    }

    public void updateScore()
    {
        score += 1;
        size = 2 * score;
        // Debug.Log(score.ToString() + "score");
    }
}
