using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{

    public bool isAnimating { get { return _isAnimating; } }
    bool _isAnimating { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Set()
    {
        _isAnimating = !_isAnimating;
    }
}
