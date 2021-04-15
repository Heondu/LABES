using UnityEngine;
using UnityEngine.UI;

public class UIBuffLifetimeViewer : MonoBehaviour
{
    private Image icon;
    private Image cooltimeImage;
    private SkillBuff buff;
    private Skill skill;

    private void Awake()
    {
        icon = GetComponent<Image>();
        cooltimeImage = transform.Find("Cooltime").GetComponent<Image>();
    }

    public void Init(SkillBuff buff, Skill skill)
    {
        this.buff = buff;
        this.skill = skill;
        icon.sprite = Resources.Load<Sprite>(skill.image);
    }

    private void Update()
    {
        cooltimeImage.fillAmount = buff.currentTime / skill.lifetime;
    }
}
