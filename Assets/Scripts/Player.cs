using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    private Movement movement;
    private PlayerInput playerInput;
    private AnimationController animationController;
    public PlayerStatus status;
    [SerializeField]
    private Transform[] weapon;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerInput = GetComponent<PlayerInput>();
        animationController = GetComponent<AnimationController>();
        LoadStatus();
        status.HP = status.maxHP;
        status.mana = status.maxMana;
        status.exp = 0;
        status.level = 1;
}

    private void Update()
    {
        animationController.Movement(playerInput.GetAxis());
        if (!IsMove()) movement.Execute(playerInput.GetAxis());

        status.CalculateDerivedStatus();
        LevelUp();
    }

    private bool IsMove()
    {
        return false;
    }

    private void LevelUp()
    {
        if (status.exp >= (int)DataManager.experience[status.level]["exp"])
        {
            status.exp -= (int)DataManager.experience[status.level]["exp"];
            status.level++;
        }
    }

    public void TakeDamage(float _value, DamageType damageType)
    {
        int value = Mathf.RoundToInt(_value);

        if (damageType == DamageType.miss) FloatingDamageManager.instance.FloatingDamage("Miss", transform.position, damageType);
        else FloatingDamageManager.instance.FloatingDamage(value.ToString(), transform.position, damageType);

        if (damageType == DamageType.normal)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.critical)
        {
            status.HP = Mathf.Max(0, status.HP - value);
        }
        else if (damageType == DamageType.heal)
        {
            status.HP = Mathf.Min(status.HP + value, status.maxHP);
        }
    }

    public Status GetStatus(string name)
    {
        return status.GetStatus(name);
    }

    public object GetValue(string name)
    {
        return status.GetValue(name);
    }

    [ContextMenu("Save Status")]
    public void SaveStatus()
    {
        JsonIO.SaveToJson(status, "PlayerStatus");
    }

    [ContextMenu("Load Status")]
    public void LoadStatus()
    {
        status = JsonIO.LoadFromJson<PlayerStatus>("PlayerStatus");
    }
}
