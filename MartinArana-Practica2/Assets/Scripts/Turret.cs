using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float ShootTimer;
    public float ShootRange;

    Transform bParent;

    BaseAgent currentTarget;

    void Start()
    {
        bParent=new GameObject("bParent").transform;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        float minDist = Mathf.Infinity;
        currentTarget = null;

        for (int i = 0; i < EnemyGlobal.Agents.Count; i++)
        {
            float dist = Vector3.Distance(EnemyGlobal.Agents[i].transform.position, transform.position);
            if (dist < ShootRange)
            {
                if (dist < minDist)
                {
                    minDist = dist;
                    currentTarget = EnemyGlobal.Agents[i];
                }
            }
        }

        if (currentTarget)
        {
            transform.up = Vector2.Lerp(transform.up,currentTarget.transform.position - transform.position,Time.deltaTime);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(ShootTimer);
            if (currentTarget)
            {
                Bullet bull = Instantiate(BulletPrefab, transform.position, transform.rotation, bParent).GetComponent<Bullet>();
                bull.Target = currentTarget.transform;
            }
        }
    }
}
