using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    void Start()
    {
        foreach(GameObject obj in objects)
        {
            Instantiate(obj).name = obj.name;
        }
    }
}
