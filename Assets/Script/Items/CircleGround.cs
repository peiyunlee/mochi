using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGround : MonoBehaviour {
	
        public float rotateDeg;

        void Update()
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, rotateDeg));
        }
}

        