using System.Collections;
using UnityEngine;

public class SkillLoader : MonoBehaviour
{
    public static SkillLoader instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void LoadSkill(GameObject executor, IStatus executorStatus, string targetTag, Skill skill, Vector3 pos, Vector3 dir)
    {
        StartCoroutine(Create(executor, executorStatus, targetTag, skill, pos, dir));
    }

    public void LoadSkill(SkillData skillData, Skill skill, Vector3 pos, Vector3 dir)
    {
        StartCoroutine(Create(skillData.executor, skillData.executorStatus, skillData.targetTag, skill, pos, dir));
    }

    private IEnumerator Create(GameObject executor, IStatus executorStatus, string targetTag, Skill skill, Vector3 pos, Vector3 dir)
    {
        if (skill.position == "pointer") pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else if (skill.position == "hero") pos += dir.normalized;
        pos.z = 0;
        GameObject skillObjcet = Resources.Load("Prefabs/Skills/" + skill.name) as GameObject;
        for (int i = 0; i < skill.repeat; i++)
        {
            GameObject clone = Instantiate(skillObjcet, pos, Quaternion.AngleAxis(Rotation.GetAngle(dir), Vector3.forward));
            SkillData[] skillDatas = clone.GetComponentsInChildren<SkillData>();
            for (int j = 0; j < skillDatas.Length; j++)
            {
                skillDatas[j].Init(executor, executorStatus, targetTag, skill);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
