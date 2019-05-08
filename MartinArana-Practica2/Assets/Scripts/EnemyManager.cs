using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class EnemyManager : MonoBehaviour
{
    public Transform NodeParent;
    public Path[] posiblePaths;

    [Header("Enemies")]
    public GameObject EnemyPrefab;
    public float SpawnTimer;

    int cc;

    void Awake()
    {
        for (int i = 0; i < NodeParent.childCount; i++)
            EnemyGlobal.Nodes[i] = NodeParent.GetChild(i);

        cc = transform.childCount;
        EnemyGlobal.Agents = new List<BaseAgent>();
        for (int i = 0; i < cc; i++)
        {
            EnemyGlobal.Agents.Add(transform.GetChild(i).GetComponent<BaseAgent>());
        }

    }

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(SpawnTimer);
            EnemyBehaviour enemy = Instantiate(EnemyPrefab, transform.position + (Vector3)Random.insideUnitCircle, transform.rotation, transform).GetComponent<EnemyBehaviour>();
            enemy.predefinedPath = posiblePaths[Random.Range(0, 2)].Nodes;

        }

    }

    void Update()
    {
        if (cc != transform.childCount)
        {
            EnemyGlobal.Agents = new List<BaseAgent>();
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyGlobal.Agents.Add(transform.GetChild(i).GetComponent<BaseAgent>());
            }
        }


        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (EnemyGlobal.paths[i + j * 8] == 1)
                    Debug.DrawLine(EnemyGlobal.Nodes[i].position, EnemyGlobal.Nodes[j].position);
    }
}

[System.Serializable]
public class Path
{
    public int[] Nodes;
}

class EnemyGlobal
{
    static public int[] paths = {        
       //0,1,2,3,4,5,6,7
    /*0*/0,1,0,0,0,0,0,0,
    /*1*/1,0,1,0,0,0,1,0,
    /*2*/0,1,0,1,0,0,0,0,
    /*3*/0,0,1,0,1,0,0,0,
    /*4*/0,0,0,1,0,1,0,0,
    /*5*/0,0,0,0,1,0,0,0,
    /*6*/0,1,0,0,0,0,0,1,
    /*7*/0,0,0,0,0,0,1,0
    };

    static public Transform[] Nodes = new Transform[8];
    static public List<BaseAgent> Agents = new List<BaseAgent>();
}
