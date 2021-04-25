using System.Collections.Generic;
using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    public void SpawnMonster(GameObject[] monsters, int[] prob, Vector2 position, int regenNumMax, float eliteSpawnPer)
    {
        GameObject enemyHolder = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyHolder"));
        List<GameObject> enemys = new List<GameObject>();
        int sumOfProb = 0;

        for (int i = 0; i < prob.Length; i++)
        {
            sumOfProb += prob[i];
        }

        for (int i = 0; i < regenNumMax; i++)
        {
            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            int index = 0;
            for (int j = 0; j < prob.Length; j++)
            {
                sum += prob[j];
                if (rand < sum)
                {
                    index = j;
                    break;
                }
            }

            GameObject clone = Instantiate(monsters[index], enemyHolder.transform);
            clone.GetComponent<Enemy>().Init();
            enemys.Add(clone);
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
                enemys[index].transform.position = newPos;
            }
        }

        enemyHolder.GetComponent<EnemySwarmController>().Init();
    }
}
