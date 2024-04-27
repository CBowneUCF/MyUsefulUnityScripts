using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BetterVectors;

public class Test : MonoBehaviour
{
    private void Start()
    {
        fun();
    }

    void fun()
    {
        BetterVectors.Vector3 v1 = new();
        BetterVectors.Vector3 v2 = new();
        if(v1 == v2) { }
    }

}
