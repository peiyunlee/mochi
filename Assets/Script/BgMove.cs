using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{

    public float speed;

    public Vector2 turn;

    public Vector2 start;

    RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rectTransform.offsetMin;

        if (pos.x <= turn.x)
        {
            rectTransform.offsetMin = new Vector2(start.x, pos.y);
            pos = rectTransform.offsetMin;
        }

        Vector2 newPos = Vector2.right * Time.deltaTime * speed + pos;

        rectTransform.offsetMin = newPos;
    }
}
