using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using S = Singleton<NewSingletonTest>;

public class NewSingletonTest : MonoBehaviour, Singleton<NewSingletonTest>
{
    public static NewSingletonTest Get() => S.Get();
    public static bool TryGet(out NewSingletonTest result) => S.TryGet(out result);

    protected static S.Delegate GetMethod = S.InitialMethods.Preloaded;
}
