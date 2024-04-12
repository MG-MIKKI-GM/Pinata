using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int fps;

    void Start()
    {
        Application.targetFrameRate = fps;
    }


    void Update()
    {
        if(Application.targetFrameRate != fps)
            Application.targetFrameRate = fps;
    }
}
