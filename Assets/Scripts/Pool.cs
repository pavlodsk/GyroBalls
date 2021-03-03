using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }
    public GameObject prefab;
    public int amount = 100;

    List<GameObject> pool = new List<GameObject>();
    private void fillPool()
    {
        for(int i = 0; i< amount; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            pool.Add(go);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        fillPool();
    }

    public GameObject getBala()
    {
        GameObject ret;
        if (pool.Count > 0)
        {
            ret = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            ret = Instantiate(prefab);
        }

        ret.SetActive(true);

        return ret;
    }

    public void returnBala(GameObject go)
    {
        go.SetActive(false);
        pool.Add(go);
    }
}
