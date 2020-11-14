using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{

    public float speed;

    public Vector2 turn;

    public Vector2 start;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x <= turn.x)
        {
            transform.position = new Vector3(start.x, transform.position.y, 0f);
            pos = transform.position;
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
