using UnityEngine;

public enum DamageType { normal = 0, critical, heal, miss }

public class FloatingDamageManager : MonoBehaviour
{
    public static FloatingDamageManager instance;
    [SerializeField]
    private GameObject[] damagePrefab;
    [SerializeField]
    private Transform canvas;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void FloatingDamage(string damage, Vector3 position, DamageType damageType)
    {
        GameObject clone = Instantiate(damagePrefab[(int)damageType], canvas);
        clone.GetComponent<FloatingDamage>().Init(damage, position);
    }
}
