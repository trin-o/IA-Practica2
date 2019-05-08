using UnityEngine;
using AI;

public class Bullet : BaseAgent
{
    public Transform Target;
    void Update()
    {
        transform.up = velocity;
        if (!Target)
        {
            return;
        }

        if (Vector3.Distance(transform.position, Target.position) < visionRange)
        {
            Target.SendMessage("Damage"/* , SendMessageOptions.DontRequireReceiver*/);
            Destroy(gameObject);
            return;
        }
        addSeek(Target.position);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
