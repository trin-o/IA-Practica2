using UnityEngine;
using AI;

public class EnemyBehaviour : BaseAgent
{
    [Header("Walk")]
    public float minDistanceToNode = 1;
    public int[] predefinedPath;

    [Header("Stats")]
    public int Health = 5;

    SpriteRenderer spr;
    int predefinedCounter = 0;
    Vector2 currentTarget;
    int currentTargetId;

    void Start()
    {
        if (predefinedPath.Length > 0)
        {
            currentTargetId = predefinedPath[predefinedCounter];
            currentTarget = EnemyGlobal.Nodes[currentTargetId].position;
        }
        spr = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        if (predefinedPath.Length > 0)
        {
            NextTarget();
            addSeek(currentTarget);
            addSeparate(EnemyGlobal.Agents, 1, .2f);
            transform.up = velocity;
        }
    }

    void NextTarget()
    {
        if (Vector2.Distance(transform.position, currentTarget) < minDistanceToNode)
        {
            if (predefinedCounter >= predefinedPath.Length)
            {
                Die();
                return;
            }

            currentTargetId = predefinedPath[predefinedCounter];
            currentTarget = EnemyGlobal.Nodes[currentTargetId].position;
            predefinedCounter++;
        }
    }

    public void Damage()
    {
        Health--;
        spr.color = Color.Lerp(Color.red, Color.white, Health / 5f);
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemyGlobal.Agents.Remove(this);
        Destroy(gameObject);
    }
}
