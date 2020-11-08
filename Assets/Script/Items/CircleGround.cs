using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircleGround : MonoBehaviour
{

    public float rotateDeg;

    // public float deg;

    // public List<Vector3Int> tileList;

    // Tilemap tilemap;

    // [SerializeField]
    // TileBase tile;

    // Matrix4x4 matrix;

    void Start()
    {
        // tilemap = GetComponent<Tilemap>();
        // deg = 0;
    }

    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, rotateDeg));

        // deg += rotateDeg;
        // if (rotateDeg >= 360)
        //     deg = 0;
        // foreach (var tileVec in tileList)
        // {
        //     tile = tilemap.GetTile(tileVec);
        //     matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, deg), Vector3.one);
        //     tilemap.SetTransformMatrix(tileVec, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, deg), Vector3.one) * matrix);
        // }
    }
}

