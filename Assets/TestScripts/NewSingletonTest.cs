using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewSingletonTest : MonoBehaviour, ISingleton<NewSingletonTest>
{
    private static NewSingletonTest _instance;
    public static NewSingletonTest Get() => ISingleton<NewSingletonTest>.Get(ref _instance);
    public static bool TryGet(out NewSingletonTest result) => ISingleton<NewSingletonTest>.TryGet(Get, out result);

}
