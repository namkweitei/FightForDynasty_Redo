using System;
using System.Collections.Generic;
using UnityEngine;
public enum PoolType
{
    Arrow,
    Stone,
    Bullet,
    Coin,
    FakeCoin,
    Lighting,
    BabylonDefault,
    BabylonCamelRiding,
    BabylonArmed,
    RomanRideHorse,
    RomanDefault,
    RomanArmed,
    EgyptianDefault,
    EgyptianArmed,
    MongoliaDefault,
    MongoliaRideElephant,
    MongoliaShoot,
    YamatoDefault,
    YamatoRideHorse,
    YamatoShoot,
    VikingDefault,
    VikingArmed,
    VikingRideWolf,
    VikingShoot,
    BossAnubisGod,
    BossDrFate,
    BossCuuVi,
    BossThanhCatTuHan,
    BossNguoiSoi,
    BossDragon,
    SkillAnubis,
    SkillDrFate,
}

public class Pool
{
    protected int nextId;
    // To make name

    Stack<GameObject> inactive;

    GameObject prefab;

    public Pool(GameObject prefabs, int initQuantify)
    {
        this.prefab = prefabs;

        //Intial stack
        inactive = new Stack<GameObject>(initQuantify);
    }

    // Method call sapwn
    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (inactive.Count == 0)
        {
            // Instatite if stack empty
            obj = GameObject.Instantiate(prefab, position, rotation);

            if (nextId >= 10)
                obj.name = prefab.name + "_" + (nextId++);
            else
                obj.name = prefab.name + "_0" + (nextId++);

            obj.AddComponent<PoolIdentify>().pool = this;
        }
        else
        {
            //inactive.Shuffle();
            obj = inactive.Pop();
            if (obj == null)
                return Spawn(position, rotation);
        }
        ISpawnable spawnable = obj.GetComponent<ISpawnable>();
        if (spawnable != null)
            spawnable.OnSpawn();
        obj.transform.SetParent(null);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        if (!inactive.Contains(obj))
            inactive.Push(obj);
        obj.SetActive(false);
    }
}

class PoolIdentify : MonoBehaviour
{
    public Pool pool;
}

public class SmartPool : Singleton<SmartPool>
{
    // Dictionary and array for GameUnit load from Resources
    public Dictionary<PoolType, GameObject> prefabs = new Dictionary<PoolType, GameObject>();
    public List<GameObject> prefabsArray;

    protected void Reset()
    {
        LoadAllGameUnit();
    }
    public GameObject Container
    {
        get
        {
            return this.gameObject;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        LoadAllGameUnit();
    }


    const int DEFAULT_POOL_SIZE = 5;

    protected Dictionary<GameObject, Pool> pools;
    // How infor pool

    // --INTIAL DICTIONARY FOR POOL--//
    protected void Init(GameObject prefabs = null, int quantify = DEFAULT_POOL_SIZE)
    {
        if (pools == null)
            pools = new Dictionary<GameObject, Pool>();

        if (prefabs != null && pools.ContainsKey(prefabs) == false)
            pools[prefabs] = new Pool(prefabs, quantify);
    }

    //--METHOD PRELOAD SOME OBJECT TO RESERVE--//
    public void Preload(GameObject prefab, int quantify)
    {
        Init(prefab, quantify);

        GameObject[] obs = new GameObject[quantify];
        for (int i = 0; i < quantify; i++)
            obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);


        for (int i = 0; i < quantify; i++)
            Despawn(obs[i]);
    }

    //--METHOD ACTIVE POOL OBJECT--//
    public GameObject Spawn(GameObject prefabs, Vector3 position, Quaternion rotarion)
    {
        Init(prefabs);
        return pools[prefabs].Spawn(position, rotarion);
    }
    public T Spawn<T>(T prefabs, Vector3 position, Quaternion rotarion) where T : GameUnit
    {
        Init(prefabs.gameObject);
        return pools[prefabs.gameObject].Spawn(position, rotarion).GetComponent<T>();
    }
    public T Spawn<T>(PoolType poolType, Vector3 position, Quaternion rotarion) where T : GameUnit
    {
        Init(prefabs[poolType]);
        return pools[prefabs[poolType]].Spawn(position, rotarion).GetComponent<T>();
    }
    public void LoadAllGameUnit()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool");
        for (int i = 0; i < gameUnits.Length; i++)
        {
            prefabs.Add(gameUnits[i].poolType, gameUnits[i].gameObject);
            prefabsArray.Add(gameUnits[i].gameObject);
        }
    }
    // Spawn GameUnit from Dictionary 

    //--METHOD DEACTIVE OBJECT POOL--
    public void Despawn(GameObject prefabs)
    {
        PoolIdentify poolIndent = prefabs.GetComponent<PoolIdentify>();

        if (poolIndent == null)
            prefabs.SetActive(false);
        else
        {
            prefabs.transform.position = Vector3.zero;
            prefabs.transform.rotation = Quaternion.identity;
            prefabs.transform.SetParent(Container.transform);
            poolIndent.pool.Despawn(prefabs);
        }
    }

    public void PreloadResource(string pathFolder, int numberPreload)
    {
        GameObject[] fxPrefabs = Resources.LoadAll<GameObject>(pathFolder);
        for (int i = 0; i < fxPrefabs.Length; i++)
            Preload(fxPrefabs[i], numberPreload);
    }

    protected object Spawn(object pfFlyText, Vector3 zero, Quaternion identity)
    {
        throw new NotImplementedException();
    }
}
public interface ISpawnable
{
    void OnSpawn();
}
