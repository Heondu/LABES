using System.Collections.Generic;
using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    public void SpawnMonster(Vector2 position, int regenNumMax, Vector2 pawnSize, Vector2 eliteSize, float eliteSpawnPer)
    {
        GameObject enemyHolder = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyHolder"));

        for (int x = 0; x < regenNumMax / 4; x++)
        {
            for (int y = 0; y < regenNumMax / 4; y++)
            {
                Vector2 size = pawnSize;
                List<object> names;
                int index;
                if (Random.Range(0f, 100f) <= eliteSpawnPer)
                {
                    size = eliteSize;
                    names = DataManager.monster.FindAll("class", "elite", "name");
                    index = Random.Range(0, names.Count);
                }
                else
                {
                    names = DataManager.monster.FindAll("class", "pawn", "name");
                    index = Random.Range(0, names.Count);
                }
                if (names.Count > 0)
                {
                    string name = names[index].ToString();

                    Vector2 newPos = position + new Vector2(size.x * x, size.y * y);
                    Vector2 offset = new Vector2(size.x * -(regenNumMax / 8), size.y * -(regenNumMax / 8));
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/Monsters/" + name), newPos + offset, Quaternion.identity);
                    clone.transform.localScale = size;
                    clone.transform.SetParent(enemyHolder.transform);
                    clone.GetComponent<Enemy>().Init(name);
                }
            }
        }

        enemyHolder.GetComponent<EnemySwarmController>().Init();
    }
}
