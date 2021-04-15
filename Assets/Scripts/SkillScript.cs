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
        if (executor == null) Destroy(gameObject);
        executorEntity = this.executor.GetComponent<ILivingEntity>();
        this.targetTag = targetTag;
        this.skill = skill;
    }

    public void SetCallBack(Callback callback)
    {
        this.callback = callback;
    }
}
