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
                StatusCalculator.CalcSkillStatus(executorEntity, entity, skill);
                penetrationCount++;
            }
            else if (skill.isPositive == 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag(targetTag) == false) continue;

                    ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                    if (entity == null) continue;
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
