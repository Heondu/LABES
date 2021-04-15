using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private Shortcut[] shortcuts;
    private Player player;
    private PlayerInput playerInput;
    private AnimationController animationController;
    public Dictionary<Skill, bool> isSkillCool = new Dictionary<Skill, bool>();
    public Dictionary<Skill, Timer> skillCool = new Dictionary<Skill, Timer>();
    public string[] skillNames;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        animationController = GetComponent<AnimationController>();

        for (int i = 0; i < skillNames.Length; i++)
        {
            InventoryManager.instance.AddSkill(DataManager.skillDB[skillNames[i]]);
        }
    }

    private void Update()
    {
        if (IsAttack(playerInput.GetSkillIndex())) Execute(shortcuts[playerInput.GetSkillIndex()].skill);
        if (hasItemSkill(playerInput.GetItemIndex())) Execute(shortcuts[playerInput.GetItemIndex()].skill);

        UpdateShortcutSkills();
    }

    private void UpdateShortcutSkills()
    {
        for (int i = 0; i < shortcuts.Length; i++)
        {
            if (shortcuts[i].skill == null) continue;
            if (isSkillCool.ContainsKey(shortcuts[i].skill) == false)
            {
                isSkillCool[shortcuts[i].skill] = false;
                skillCool[shortcuts[i].skill] = new Timer();
            }
        }
    }

    private bool IsAttack(int index)
    {
        if (index == -1) return false;
        if (shortcuts[index].skill == null) return false;
        if (isSkillCool[shortcuts[index].skill]) return false;
        return true;
    }

    private bool hasItemSkill(int index)
    {
        if (index == -1) return false;
        if (shortcuts[index].skill == null) return false;
        if (isSkillCool[shortcuts[index].skill]) return false;
        return true;
    }

    public void Execute(Skill skill)
    {
        SkillLoader.SkillLoad(gameObject, "Enemy", skill, transform.position);
        isSkillCool[skill] = true;
        skillCool[skill] = new Timer();
        StartCoroutine("Cooltime", skill);
        animationController.Attack();
    }

    private IEnumerator Cooltime(Skill skill)
    {
        while (skillCool[skill].IsTimeOut(Mathf.Max(0.3f, skill.cooltime - player.GetStatus("reduceCool").Value / 10)) == false)
        {
            yield return null;
        }
        isSkillCool[skill] = false;
    }
}
