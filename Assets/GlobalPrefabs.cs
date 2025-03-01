#define AYellowPaper

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using S = ISingleton<GlobalPrefabs>;

[CreateAssetMenu(fileName = "Global Prefabs", menuName = "Global Prefabs", order = 0)]
public class GlobalPrefabs : ScriptableObject, ISingleton<GlobalPrefabs>
{
    private static GlobalPrefabs _instance;
    public static GlobalPrefabs Get() => S.Get(ref _instance);
    public static bool TryGet(ref GlobalPrefabs _instance) => S.TryGet(Get, out _instance);

    void Awake() => (this as S).Initialize(ref _instance);
    void OnEnable() => (this as S).Initialize(ref _instance);

    void ISingleton<GlobalPrefabs>.OnInitialize()
    {
#if AYellowPaper
#else
        dictionary = new();
        for (int i = 0; i < NamedPrefabs.Length; i++)
            dictionary.Add(PrefabNames[i], NamedPrefabs[i]);
#endif
    }

    public List<Singleton> singletons;
    public static List<Singleton> Singletons => Get().singletons;

#if AYellowPaper
    [SerializeField] AYellowpaper.SerializedCollections.SerializedDictionary<string, GameObject> dictionary;
#else

    [SerializeField] GameObject[] NamedPrefabs;
    [SerializeField] string[] PrefabNames;
    private Dictionary<string, GameObject> dictionary;

#endif

    public GameObject this[string name] => Get().dictionary[name];
    public static GameObject NamedPrefab(string name) => Get().dictionary[name];
    public static bool TryNamedPrefab(string name, out GameObject result) => Get().dictionary.TryGetValue(name, out result);








}