using UnityEngine;

public class Health : MonoBehaviour
{
    private int HP;
    [SerializeField]
    private int maxHP;


    private void Awake()
    {
        HP = maxHP;
    }

    public int HPCalc(int value, bool isAdd)
    {
        if (isAdd) return HP = Mathf.Min(maxHP, HP + value);
        else return HP = Mathf.Max(0, HP - value);
    }
}
