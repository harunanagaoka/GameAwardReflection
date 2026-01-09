using UnityEngine;

//“G‚Ì¶¬
public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnEnemy(EnemyData enemy)
    {
        GameObject obj = Instantiate(enemy.Prefab,enemy.InitPos,enemy.Prefab.transform.rotation,transform);

        return obj;
    }
}
