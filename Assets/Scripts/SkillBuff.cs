using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillData))]
public class SkillBuff : MonoBehaviour
{
    [SerializeField]
    protected float radius;
    protected List<ILivingEntity> entityList = new List<ILivingEntity>();
    protected Transform buffHolder;
    protected Transform buffUIHolder;
    protected GameObject buffPrefab;
    protected SkillData skillData;
    protected SkillLifetime skillLifetime;

    protected virtual void Awake()
    {
        buffHolder = GameObject.Find("BuffHolder").transform;
        buffUIHolder = GameObject.Find("Buff").transform;
        skillData = GetComponent<SkillData>();
        skillLifetime = GetComponent<SkillLifetime>();
        buffPrefab = Resources.Load<GameObject>("Prefabs/UI/Buff");
    }

    protected virtual void Start()
    {
        Transform buff = buffHolder.Find(gameObject.name);
        if (buff != null)
        {
            buff.GetComponent<SkillLifetime>().ResetTime();
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(buffHolder);
            Execute();
        }
    }

    protected virtual void Execute()
    {
        Skill skill = skillData.skill;
        List<GameObject> targets = new List<GameObject>();
        if (skill.isPositive == 1)
        {
            targets.Add(skillData.executor);
        }
        else if (skill.isPositive == 0)
        {
            targets = FindAllTarget(radius);
        }
        foreach (GameObject target in targets)
        {
            ILivingEntity targetEntity = target.GetComponent<ILivingEntity>();
            entityList.Add(targetEntity);
            for (int i = 0; i < skill.repeat; i++)
            {
                StatusCalculator.CalcSkillStatus(skillData.executorStatus, targetEntity, skill, skillData.GetStatus, skillData.GetRelatedStatus);
            }
        }

        GameObject clone = Instantiate(buffPrefab, buffUIHolder);
        clone.GetComponent<UIBuffLifetimeViewer>().Init(skill, skillLifetime);
    }

    protected List<GameObject> FindAllTarget(float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        List<GameObject> targetList = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(skillData.targetTag)) targetList.Add(collider.gameObject);
        }
        return targetList;
    }

    private void OnDestroy()
    {
        foreach (ILivingEntity entity in entityList)
        {
            for (int i = 0; i < 2; i++)
            {
                Status status = entity.GetStatus(skillData.GetStatus);
                if (status != null) status.RemoveAllModifiersFromSource(skillData.skill);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
