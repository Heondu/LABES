using UnityEngine;

public class RegenArea : MonoBehaviour
{
    public Vector2 position;
    public int maxRegenNum;
    public Vector2 pawnSize;
    public Vector2 eliteSize;
    public float eliteSpawnPer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(position, 0.5f);
    }
}
