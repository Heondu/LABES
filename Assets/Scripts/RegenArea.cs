using UnityEngine;

public class RegenArea : MonoBehaviour
{
    public Vector2 position;
    public int maxRegenNum;

    public GameObject[] monsters;
    public int[] prob;

    public float eliteSpawnPer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(position, 0.5f);
    }
}
