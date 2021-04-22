using System.Collections.Generic;
using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    public void SpawnMonster(Vector2 position, int regenNumMax, float eliteSpawnPer)
    {
        GameObject enemyHolder = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyHolder"));
        List<GameObject> enemys = new List<GameObject>();

        for (int i = 0; i < regenNumMax; i++)
        {
            List<object> names;
            int index;
            do
            {
                if (Random.Range(0f, 100f) <= eliteSpawnPer)
                {
                    names = DataManager.monster.FindAll("class", "elite", "name");
                    index = Random.Range(0, names.Count);
                }
                else
                {
                    names = DataManager.monster.FindAll("class", "pawn", "name");
                    index = Random.Range(0, names.Count);
                }
            } while (names.Count <= 0);

            if (names.Count > 0)
            {
                string name = names[index].ToString();
                GameObject clone = Instantiate(Resources.Load<GameObject>("Prefab/Monsters/Mon000/" + name), enemyHolder.transform);
                clone.GetComponent<Enemy>().Init(name);
                enemys.Add(clone);
            }
        }

        int enemyColumn = (int)Mathf.Sqrt(regenNumMax);
        int enemyRow = Mathf.CeilToInt(Mathf.Sqrt(regenNumMax));
        for (int x = 0; x < enemyRow; x++)
        {
            for (int y = 0; y < enemyColumn; y++)
            {
                int index = x * enemyColumn + y;
                if (index >= regenNumMax) continue;

                Bounds enemyBounds = enemys[index].GetComponent<CapsuleCollider2D>().bounds;
                Vector2 newPos = position + new Vector2(enemyBounds.size.x * x, enemyBounds.size.y * y);
                //Vector2 offset
                enemys[index].transform.position = newPos;
            }
        }

        enemyHolder.GetComponent<EnemySwarmController>().Init();
    }
}
