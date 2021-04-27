using System.Collections;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private const float FIND_DISTANCE = 8f;
    private const float CAHSE_DISTANCE = 13f;
    private const float ATTACK_DISTANCE = 8f;
    private Timer timer = new Timer();
    private Vector3[] patrolDir = { Vector3.right, Vector3.zero, Vector3.down, Vector3.zero,
                                    Vector3.left, Vector3.zero, Vector3.up, Vector3.zero };
    private int currentDirNum = 0;
    private const float PATROL_TIME = 1f;
    public bool isSwarmAttack = false;
    private EnemyState state = EnemyState.STATE_PATROL;
    private bool isStop = false;
    private bool canChange = true;
    private float changeCool = 1f;

    private void Awake()
    {
        target = FindObjectOfType<Player>().gameObject;
        RandSort();
    }

    public void Update()
    {
        FSM();
    }

    private void FSM()
    {
        if (isStop) return;

        if (state == EnemyState.STATE_PATROL)
        {
            if (Distance() <= FIND_DISTANCE) SetState(EnemyState.STATE_CHASE);
            if (isSwarmAttack == true) SetState(EnemyState.STATE_ATTACK);
        }
        if (state == EnemyState.STATE_CHASE)
        {
            if (Distance() > CAHSE_DISTANCE && isSwarmAttack == false) SetState(EnemyState.STATE_PATROL);
            if (Distance() <= ATTACK_DISTANCE || isSwarmAttack == true) SetState(EnemyState.STATE_ATTACK);
        }
        if (state == EnemyState.STATE_ATTACK)
        {
            if (Distance() > CAHSE_DISTANCE && isSwarmAttack == false) SetState(EnemyState.STATE_PATROL);
            if (Distance() > ATTACK_DISTANCE) SetState(EnemyState.STATE_CHASE);
        }
    }

    //private bool IsPatrol()
    //{
    //    if (isStop) return false;
    //    if (Distance() > CAHSE_DISTANCE && isSwarmAttack == false) return true;
    //    return false;
    //}
    //
    //private bool IsChase()
    //{
    //    if (isStop) return false;
    //    if (Distance() <= CAHSE_DISTANCE && IsAttack() == false) return true;
    //    if (isSwarmAttack == true && IsAttack() == false) return true;
    //    return false;
    //}
    //
    //private bool IsAttack()
    //{
    //    if (isStop) return false;
    //    if (Distance() <= ATTACK_DISTANCE) return true;
    //    return false;
    //}

    public Vector3 GetAxis()
    {
        if (isStop) return Vector3.zero;
        if (state == EnemyState.STATE_PATROL || state == EnemyState.STATE_ATTACK)
        {
            if (timer.IsTimeOut(PATROL_TIME))
            {
                RandSort();
                currentDirNum = (currentDirNum + 1) % patrolDir.Length;
            }
            return patrolDir[currentDirNum];
        }
        else if (state == EnemyState.STATE_CHASE) return (target.transform.position - transform.position).normalized;
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

    public EnemyState GetState()
    {
        return state;
    }

    public void SetState(EnemyState state)
    {
        if (canChange)
        {
            this.state = state;
            StartCoroutine("ChangeCool");
        }
    }

    public IEnumerator Stop(float duration)
    {
        isStop = true;

        yield return new WaitForSeconds(duration);

        isStop = false;
    }

    private IEnumerator ChangeCool()
    {
        canChange = false;

        yield return new WaitForSeconds(changeCool);

        canChange = true;
    }

    public Vector2 GetAttackDir()
    {
        return (target.transform.position - transform.position).normalized;
    }
}
