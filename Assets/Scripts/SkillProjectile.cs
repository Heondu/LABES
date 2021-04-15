using System.Collections;
using UnityEngine;

public class SkillProjectile : SkillScript
{
    [SerializeField]
    private float radius;
    private GameObject target;
    private Movement movement;
    private int penetrationCount = 0;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        if (skill != null)
        {
            if (timer.IsTimeOut(skill.lifetime)) Destroy(gameObject);
        }
        movement.Execute(transform.up, skill.speed);
        SetDir();
    }

    public override void Execute(GameObject executor, string targetTag, Skill skill)
    {
        base.Execute(executor, targetTag, skill);
        float angle = 0;
        if (targetTag == "Enemy") angle = Rotation.GetAngle(executor.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        else if (targetTag == "Player") angle = Rotation.GetAngle(executor.transform.position, GameObject.FindWithTag(targetTag).transform.position);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position += transform.up * 1;
        StartCoroutine("FindTarget");
    }

    private void SetDir()
    {
        if (target == null) return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * skill.guide);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            target = null;
            float distance = Mathf.Infinity;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag(targetTag) == false) continue;

                ILivingEntity entity = collider.GetComponent<ILivingEntity>();
                if (entity == null) continue;
                if (distance > Vector3.SqrMagnitude(collider.transform.position - transform.position))
                {
                    target = collider.gameObject;
                    distance = Vector3.SqrMagnitude(target.transform.position - transform.position);
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            penetrationCount++;
            for (int i = 0; i < nextSkills.Length; i++)
                if (nextSkills[i] != "") SkillLoader.SkillLoad(executor, targetTag, DataManager.skillDB[nextSkills[i]], transform.position);
            if (skill.penetration <= penetrationCount) Destroy(gameObject);
        }
    }
}
