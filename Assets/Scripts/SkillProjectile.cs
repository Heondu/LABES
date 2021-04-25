using UnityEngine;

[RequireComponent(typeof(SkillData))]
[RequireComponent(typeof(ProjectileMove))]
public class SkillProjectile : MonoBehaviour
{
    protected GameObject target;
    protected int penetrationCount = 0;
    protected SkillData skillData;
    protected ProjectileMove projectileMove;
    [SerializeField]
    protected string[] nextSkills;

    protected virtual void Awake()
    {
        skillData = GetComponent<SkillData>();
        projectileMove = GetComponent<ProjectileMove>();
    }

    protected virtual void Start()
    {
        projectileMove.SetSpeed(skillData.speed);
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void Execute()
    {
        for (int i = 0; i < nextSkills.Length; i++)
        {
            SkillLoader.instance.LoadSkill(skillData, DataManager.skillDB[nextSkills[i]], transform.position, transform.up);
        }

        penetrationCount++;
        if (penetrationCount >= skillData.penetration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(skillData.targetTag))
        {
            Execute();
        }
    }
}
