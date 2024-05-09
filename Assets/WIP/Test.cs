using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BetterVectors;
using AYellowpaper.SerializedCollections;

public class Test : MonoBehaviour
{
    public SerializedDictionary<string, int> test;
    public BetterVectors.Vector3 position = new(1, 2, 4);

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
