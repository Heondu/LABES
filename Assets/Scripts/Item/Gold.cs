using System.Collections;
using UnityEngine;

public class Gold : MonoBehaviour, IItem
{
    private new Rigidbody2D rigidbody;

    private int gold = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Diffusion(int weight)
    {
        Vector2 dir = Random.insideUnitCircle;

        rigidbody.mass = weight;

        rigidbody.AddForce(dir * 10, ForceMode2D.Impulse);
    }

    public void Use()
    {
        InventoryManager.instance.AddGold(gold);
        Destroy(gameObject);
    }

    public void SetGold(int value)
    {
        gold = value;
    }
}
