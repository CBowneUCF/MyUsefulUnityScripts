using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    private void Awake()
    {
        if (NewSingletonTest.TryGet(out NewSingletonTest result))
        {
            Debug.Log("Yes");
        }
    }

}
