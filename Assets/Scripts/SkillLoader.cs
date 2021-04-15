using UnityEngine;

public class SkillLoader : MonoBehaviour
{
    [SerializeField]
    private static Transform buffHolder;

    public static SkillScript SkillLoad(GameObject executor, string targetTag, Skill skill, Vector3 pos)
    {
        if (skill.position == "pointer") pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        GameObject clone = Instantiate(Resources.Load("Prefabs/Skills/" + skill.name) as GameObject, pos, Quaternion.identity);
        if (skill.type == "buff") clone.transform.parent = buffHolder;
        SkillScript skillClone = clone.GetComponent<SkillScript>();
        skillClone.Execute(executor, targetTag, skill);

        return skillClone;
    }
}
