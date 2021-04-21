using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { STATE_NULL = 0, STATE_PATROL, STATE_CHASE, STATE_ATTACK }

public class Enemy : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private EnemyController enemyController;
    private EnemyAttack enemyAttack;
    private AnimationController animationController;
    private EnemyState state = EnemyState.STATE_PATROL;
    private new string name;
    public EnemyStatus status;
    public Dictionary<string, object> monster = new Dictionary<string, object>();
    public Dictionary<string, object> monlvl = new Dictionary<string, object>();
    private float delay;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        enemyController = GetComponent<EnemyController>();
        enemyAttack = GetComponent<EnemyAttack>();
        animationController = GetComponent<AnimationController>();
    }

    private void Update()
    {
        ChangeState(enemyController.Operate());

        switch (state)
        {
            case EnemyState.STATE_PATROL:
                movement.Execute(enemyController.GetAxis());
                animationController.Movement(enemyController.GetAxis());
                break;
            case EnemyState.STATE_CHASE:
                movement.Execute(enemyController.GetAxis());
                animationController.Movement(enemyController.GetAxis());
                break;
            case EnemyState.STATE_ATTACK:
                enemyAttack.Execute(delay);
                animationController.Attack();
                break;
        }
    }

    private void ChangeState(EnemyState state)
    {
        this.state = state; 
    }

    public void Init(string name)
    {
        this.name = name;
        monster = DataManager.monster.FindDic("name", name);
        monlvl = DataManager.monlvl.FindDic("Level", monster["monlvl"]);
        status.maxHP = 30;
        status.HP = status.maxHP;
        status.strength.BaseValue = (int)monlvl["strength"];
        status.agility.BaseValue = (int)monlvl["agility"];
        status.intelligence.BaseValue = (int)monlvl["intelligence"];
        status.endurance.BaseValue = (int)monlvl["endurance"];
        status.CalculateDerivedStatus();
        enemyAttack.SkillInit(monster);
        delay = float.Parse(monster["delay"].ToString());
    }

    public void TakeDamage(float _value, DamageType damageType)
    {
        enemyController.isSwarmAttack = true;

        int value = Mathf.RoundToInt(_value);

        if (damageType == DamageType.miss) FloatingDamageManager.instance.FloatingDamage("Miss", transform.position, damageType);
        else FloatingDamageManager.instance.FloatingDamage(value.ToString(), transform.position, damageType);

        if (damageType == DamageType.normal) status.HP = Mathf.Max(0, status.HP - value);
        else if (damageType == DamageType.critical) status.HP = Mathf.Max(0, status.HP - value);
        else if (damageType == DamageType.heal) status.HP = Mathf.Min(status.HP + value, status.maxHP);

        if (status.HP == 0)
        {
            FindObjectOfType<Player>().status.exp += (int)monlvl["monexp"];
            ItemGenerator.instance.DropItem((int)monlvl["raritymin"], (int)monlvl["raritymax"], monster["class"].ToString(), transform.position);
            Destroy(gameObject);
        }
    }

    public Status GetStatus(string name)
    {
        return status.GetStatus(name);
    }
}
