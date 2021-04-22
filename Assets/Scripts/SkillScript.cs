using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    [SerializeField]
    protected string[] nextSkills;
    public delegate void Callback(Skill skill);
    protected Callback callback = null;
    protected Timer timer = new Timer();
    protected GameObject executor;
    protected ILivingEntity executorEntity;
    protected Skill skill;
    protected string targetTag;

    protected virtual void Update()
    {

    }

    public virtual void Execute(GameObject executor, string targetTag, Skill skill)
    {
        this.executor = executor;
        if (this.executor == null) Destroy(gameObject);
        else executorEntity = this.executor.GetComponent<ILivingEntity>();
        this.targetTag = targetTag;
        this.skill = skill;
    }

    public void SetCallBack(Callback callback)
    {
        this.callback = callback;
    }

    protected GameObject FindTarget(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        GameObject target = null;
        float distance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(targetTag) == false) continue;

            float newDistance = Vector3.SqrMagnitude(collider.transform.position - transform.position);
            if (distance > newDistance)
            {
                target = collider.gameObject;
                distance = newDistance;
            }
        }
        return target;
    }

    protected List<GameObject> FindAllTarget(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        List<GameObject> targetList = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(targetTag)) targetList.Add(collider.gameObject);
        }
        return targetList;
    }
}
