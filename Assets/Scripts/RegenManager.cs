using UnityEngine;

public class RegenManager : MonoBehaviour
{
    private void Awake()
    {
        Spawn();
    }

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        GameObject[] regens = GameObject.FindGameObjectsWithTag("RegenArea");
        foreach (GameObject regen in regens)
        {
            RegenArea regenArea = regen.GetComponent<RegenArea>();
            regen.GetComponent<RegenMonster>().SpawnMonster(regenArea.position, regenArea.maxRegenNum, regenArea.eliteSpawnPer);
        }
    }
}
