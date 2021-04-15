using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private const float CAHSE_DISTANCE = 5f;
    private const float ATTACK_DISTANCE = 3f;
    private Timer timer = new Timer();
    private Vector3[] patrolDir = { Vector3.right, Vector3.zero, Vector3.down, Vector3.zero,
                                    Vector3.left, Vector3.zero, Vector3.up, Vector3.zero };
    private int currentDirNum = 0;
    private const float PATROL_TIME = 1f;
    public bool isSwarmAttack = false;

    private void Awake()
    {
        target = FindObjectOfType<Player>().gameObject;
        RandSort();
    }

    public EnemyState Operate()
    {
        if (IsPatrol()) return EnemyState.STATE_PATROL;
        if (IsChase()) return EnemyState.STATE_CHASE;
        if (IsAttack()) return EnemyState.STATE_ATTACK;
        return EnemyState.STATE_NULL;
    }

    private bool IsPatrol()
    {
        if (target == null) return true;
        if (Distance() > CAHSE_DISTANCE && isSwarmAttack == false) return true;
        return false;
    }

    private bool IsChase()
    {
        if (Distance() <= CAHSE_DISTANCE && Distance() > ATTACK_DISTANCE || isSwarmAttack == true) return true;
        return false;
    }

    private bool IsAttack()
    {
        if (Distance() <= ATTACK_DISTANCE) return true;
        return false;
    }

    public Vector3 GetAxis()
    {
        if (IsPatrol())
        {
            if (timer.IsTimeOut(PATROL_TIME))
            {
                RandSort();
                currentDirNum = (currentDirNum + 1) % patrolDir.Length;
            }
            return patrolDir[currentDirNum];
        }
        else if (IsChase()) return (target.transform.position - transform.position).normalized;
        return Vector3.zero;
    }

    public float Distance()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    private void RandSort()
    {
        if (currentDirNum != 0) return;
        for (int i = 0; i < patrolDir.Length; i++)
        {
            int randNum = Random.Range(0, patrolDir.Length);
            Vector3 temp = patrolDir[randNum];
            patrolDir[randNum] = patrolDir[i];
            patrolDir[i] = temp;
        }
    }
}
