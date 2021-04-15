using System.Collections;
using System.Collections.Generic;
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
            regen.AddComponent<RegenMonster>().SpawnMonster(regenArea.position, regenArea.maxRegenNum, regenArea.pawnSize, regenArea.eliteSize, regenArea.eliteSpawnPer);
        }
    }
}
