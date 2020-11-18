using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{


    public float w;//椭圆长
    public float h; //椭圆高
    public int angle = 360;
    [Range(0, 360)]
    private Vector3[] vec;
    private LineRenderer line;
    float x, y;
    UnityJellySprite jellySprite;

	Transform trans;

    // Use this for initialization
    void Start()
    {
		w = 0.45f;
		h = 0.36f;
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
		trans = jellySprite.CentralPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        vec = new Vector3[angle];
        for (int i = 0; i < angle; i++)
        {
            // Mathf.Deg2Rad 单位角度的弧 相当于 1° 的弧度
            x = w * Mathf.Cos(i * Mathf.Deg2Rad);
            y = h * Mathf.Sin(i * Mathf.Deg2Rad);
            vec[i] = trans.position + new Vector3(x, y, 0);
        }
        SetLine();
    }

    void SetLine()
    {

        line = gameObject.GetComponent<LineRenderer>();
        //设置线由多少个点构成
        line.positionCount = angle;
        //绘制点的坐标
        line.SetPositions(vec);
    }
}
