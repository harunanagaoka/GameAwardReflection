using UnityEngine;

//“G‚Ì¶¬
public class EnemySpawner : MonoBehaviour
{
    public EnemyDamageable SpawnEnemy(EnemyData enemy)
    {
        GameObject obj = Instantiate(enemy.Prefab,enemy.InitPos,Quaternion.identity);//e‚Æ‚©‚Í‚ ‚Æ‚©‚çl‚¦‚é

        return obj.GetComponent<EnemyDamageable>();
    }
}
