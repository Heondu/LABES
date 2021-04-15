using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SkillBuff : SkillScript
{
    private List<ILivingEntity> entityList = new List<ILivingEntity>();
    private Transform buffHolder;
    private Transform buffUIHolder;
    private GameObject buffPrefab;
    public float currentTime = 0;
    private GameObject clone;

    private void Awake()
    {
        buffHolder = GameObject.Find("BuffHolder").transform;
        buffUIHolder = GameObject.Find("Buff").transform;
        buffPrefab = Resources.Load<GameObject>("Prefabs/UI/Buff");
    }

    public override void Execute(GameObject executor, string targetTag, Skill skill)
    {
        base.Execute(executor, targetTag, skill);

        Transform buff = buffHolder.Find(gameObject.name);
        if (buff != null)
        {
            buff.GetComponent<SkillBuff>().currentTime = 0;
            Destroy(gameObject);
            return;
        }

        if (skill.isPositive == 1)
        {
            ILivingEntity entity = executor.GetComponent<ILivingEntity>();
            entityList.Add(entity);
            StatusCalculator.CalcSkillStatus(executorEntity, entity, skill);
        }
        else if (skill.isPositive == 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill.size);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag(targetTag) == false) continue;

                ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                if (entity == null) continue;
                entityList.Add(entity);
                StatusCalculator.CalcSkillStatus(executorEntity, entity, skill);
            }
        }
        transform.parent = buffHolder;
        clone = Instantiate(buffPrefab, buffUIHolder);
        clone.GetComponent<UIBuffLifetimeViewer>().Init(this, skill);
        StartCoroutine("LifeTime");
    }

    public IEnumerator LifeTime()
    {
        currentTime = 0;
        while (currentTime < skill.lifetime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(clone);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        foreach (ILivingEntity entity in entityList)
        {
            for (int i = 0; i < 2; i++)
            {
                Status status = entity.GetStatus(skill.status[i]);
                if (status != null) status.RemoveAllModifiersFromSource(skill);
            }
        }
    }
}
