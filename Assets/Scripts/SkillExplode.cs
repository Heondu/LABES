using System.Collections;
using UnityEngine;

public class SkillExplode : SkillScript
{
    [SerializeField]
    private float radius;
    private int penetrationCount = 0;

    public override void Execute(GameObject executor, string targetTag, Skill skill)
    {
        base.Execute(executor, targetTag, skill);
        StartCoroutine("CoExecute");
    }

    private IEnumerator CoExecute()
    {
        while (true)
        {
            if (skill.isPositive == 1)
            {
                ILivingEntity entity = executor.GetComponent<ILivingEntity>();
                for (int i = 0; i < skill.repeat; i++)
                    StatusCalculator.CalcSkillStatus(executorEntity, entity, skill);
                penetrationCount++;
            }
            else if (skill.isPositive == 0)
            {
                foreach (GameObject target in FindAllTarget(radius))
                {
                    ILivingEntity entity = target.GetComponent<ILivingEntity>();
                    if (entity == null) continue;
                    for (int i = 0; i < skill.repeat; i++)
                        StatusCalculator.CalcSkillStatus(executorEntity, entity, skill);
                    penetrationCount++;
                }
            }

            if (skill != null)
            {
                if (timer.IsTimeOut(skill.lifetime)) Destroy(gameObject);
                if (skill.penetration <= penetrationCount) Destroy(gameObject);
            }

            yield return new WaitForSeconds(skill.delay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
