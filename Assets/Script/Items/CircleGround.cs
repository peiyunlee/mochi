﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Tilemaps;

public class CircleGround : MonoBehaviour
{
    public float rotateDeg;
    float rotateAngle;
    public float r;

    public float h, k;

    public List<GameObject> childList;

    public GameObject sprite;

    List<Transform> transList;
    public List<Rigidbody2D> rbList;

    float eachAngle;


    float angle;

    public float m;

    Vector2 pre, next;

    void Start()
    {
        angle = 0;
        rotateAngle = (Mathf.PI / 180) * rotateDeg;

        eachAngle = 2f * Mathf.PI / childList.Count;

        float eachDeg = 360 / childList.Count;

        int index = 0;
        foreach (var item in childList)
        {
            float x = r * Mathf.Cos(eachAngle * index) + h;
            float y = r * Mathf.Sin(eachAngle * index) + k;
            childList[index].transform.position = new Vector2(x, y);
            childList[index].transform.Rotate(new Vector3(0, 0, eachDeg * index));
            index++;
        }

        //tile式移動
        // tilemap = GetComponent<Tilemap>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < childList.Count; i++)
        {
            Rotate(i);
        }
        angle += rotateAngle * Time.deltaTime;

        sprite.transform.Rotate(new Vector3(0, 0, rotateDeg * Time.deltaTime));
    }

    void Rotate(int index)
    {
        Vector3 pos = childList[index].transform.position;

        MoveRigibody(index);
    }

    void MoveRigibody(int index)
    {
        next = pre;

        angle += rotateAngle * Time.deltaTime;

        float x = r * Mathf.Cos(eachAngle * index + angle) + h;
        float y = r * Mathf.Sin(eachAngle * index + angle) + k;

        pre = new Vector2(x, y);

        rbList[index].MovePosition(pre);

        rbList[index].MoveRotation(angle * m);

        angle -= rotateAngle * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            GameObject player = other.gameObject.GetComponentInParent<PlayerMovement>().gameObject;
            if (player != null)
            {
                Vector2 slop = pre - next;
                player.GetComponent<UnityJellySprite>().AddVelocity(slop*15.0f, false);
            }

        }
    }
}

