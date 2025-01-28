using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// A type of Behavior that can only exist once in a scene. <br/>
/// Basic form that functions out of the box. (Inheret from SingletonAdvanced instead for special functionality.)
/// </summary>
/// <typeparam name="T">The Behavior's Type</typeparam>
public interface Singleton<T> : SingletonBase where T : UnityEngine.Object, Singleton<T>, new()
{
    
    private static T _instance;

    public delegate bool Delegate(out T Return);
    protected static Delegate GetMethodP;

    public static T Get() => InitialMethods.AttemptGet(GetMethodP);
    public static bool TryGet(out T output)
    {
        output = InitialMethods.AttemptGet(GetMethodP);
        return output != null;
    }

    public static bool Active => _instance != null;




    public static class InitialMethods
    {

        public static T AttemptGet(Delegate secondMethod)
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) { Debug.LogError($"{typeof(T)} accessed outside of runtime. Don't."); return null; }
            #endif
            if (_instance != null) return _instance;

            if (FindObject(out T attempt1))
            {
                attempt1.Initialize();
                return _instance;
            }
            if (secondMethod != null && secondMethod.Invoke(out T attempt2))
            {
                attempt2.Initialize();
                return _instance;
            }
            throw new Exception($"No Singleton of type {typeof(T)} could be found.");
        }

        /// <summary>
        /// Most Basic Initialization Method. Simply attempts to find it in loaded scene or among loaded Scriptable Objects. <br />
        /// Automatically run first before any other Initial Methods. <br />
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Success Value</returns>
        public static bool FindObject(out T result)
        {
            T findAttempt = UnityEngine.Object.FindFirstObjectByType<T>(FindObjectsInactive.Include);
            if (findAttempt != null)
            {
                result = findAttempt;
                result.Initialize();
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public static bool Create(out T result)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
            {
                GameObject GO = new(typeof(T).ToString());
                result = GO.AddComponent(typeof(T)) as T;
                return true;
            }
            else if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
            {
                result = ScriptableObject.CreateInstance(typeof(T)) as T;
                return true;
            }
            result = null;
            throw new Exception("Unable to create instance of Singleton for some reason.");
        }


        public static bool ResourcePrefabPath(out T result)
        {
            if (typeof(T).ImplementsOrDerives(typeof(Path)))
            {
                result = UnityEngine.Object.Instantiate(Resources.Load<T>((new T() as Path).Path));
                return true;
            }
            else throw new Exception("This Singleton type doesn't have a path attached. Add SingletonPath Interface.");
            throw new Exception("Unable to create instance of Singleton for some reason.");
        }

        public static bool SavedPrefab(out T result)
        {
            result = UnityEngine.Object.Instantiate(GlobalPrefabs.Singletons.FirstOrDefault(x => x is T) as T);
            return true;

        }

        public static bool Preloaded(out T result)
        {
            result = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<T>()[0]);
            return true;
        }

#if UNITY_ADDRESSABLES_EXIST
        /// <summary>
        /// Instantiates a Prefab using the Addressables System. (Make sure to set the path in SetInfo.)
        /// </summary>
        /// <returns></returns>
        public static bool AddressablePrefab(out T result)
        {
            //NOTE, NEEDS MORE ERROR PROOFING. ADD LATER.
            result = UnityEngine.Object.Instantiate(UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(((new T() as Path).Path)).WaitForCompletion()) as T;
            return true;
        }
#endif



    }









    protected void Initialize()
    {
        if (_instance != null || _instance == this) return;
        else if (_instance != null && _instance != this)
        {
            #if UNITY_EDITOR
            Debug.Log($"Second {typeof(T)} found, Destroying...");
            #endif

            UnityEngine.Object.Destroy(this as T);
        }
        else
        {
            _instance = this as T;
            OnInitialize();
        }
    }
    protected virtual void OnInitialize() { }

    protected void DeInitialize()
    {
        if (_instance == this)
        {
            _instance = null;
            OnDeInitialize();
        }
    }
    protected virtual void OnDeInitialize() { }







    public interface Path
    {
        public string Path => throw new NotImplementedException();
    }
     
}

public interface SingletonBase
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Boot()
    {
        Type[] types = typeof(Singleton<>).GetAllChildTypes(false);
    
        foreach (Type T in types)  
        {
            if (T.IsInterface) continue;

            FieldInfo I = T.GetField("GetMethod", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);
            if (I == null) continue;
            FieldInfo P = T.GetInterfaces()
                            .First(x => x.ImplementsOrDerives(typeof(Singleton<>)))
                            .GetField("GetMethodP", BindingFlags.Static | BindingFlags.NonPublic);

            P.SetValue(null, I.GetValue(null));
        }
    }
}

/* Example Use --------------------------------------------------------------------------------------------------------------------------------------------

public class ExampleSingleton : SingletonAdvanced<ExampleSingleton>
{
    static void Data() => SetData(spawnMethod: InitResourcePrefab, dontDestroyOnLoad: true, spawnOnBoot: true, path: "ExampleSingleton");
}

Spawn methods include:

InitFind
--------Simply Attempts to Find the Singleton in the current scene.

InitCreate
----------Creates an instance of the Singleton from scratch.

InitResourcePrefab
------------------Instantiates a Prefab from the Resources folder. (Make sure to set the path in SetInfo.)

InitSavedPrefab
---------------Instantiates a Prefab from the GlobalPrefabs ScriptableSingleton. (Make sure to place exactly one prefab into said Scriptable Object.)

InitAddressablePrefab
---------------------Instantiates a Prefab using the Addressables System. (Make sure to set the path in SetInfo.)



 */

public static class TypeHelpers
{
    public static Type[] GetAllChildTypes(this Type T, bool noAbstracts = false)
    => Assembly.GetAssembly(T).GetTypes().Where(i =>
    i.ImplementsOrDerives(T) &&
    (!noAbstracts || !i.IsAbstract())
    ).ToArray();

    public static bool ImplementsOrDerives(this Type @this, Type from)
    {
        if (from is null)
            return false;

        if (!from.IsGenericType || !from.IsGenericTypeDefinition)
            return from.IsAssignableFrom(@this);

        if (from.IsInterface)
            foreach (Type @interface in @this.GetInterfaces())
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == from)
                    return true;

        if (@this.IsGenericType && @this.GetGenericTypeDefinition() == from)
            return true;

        return @this.BaseType?.ImplementsOrDerives(from) ?? false;
    }
}