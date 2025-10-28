using UnityEngine;

//using static BulletBehaviourData;

public class EnemyGenerator : MonoBehaviour
{
    //[SerializeField]
    //private GameObject m_baseBullet;

    //private ConfigData.BulletConfig m_bulletConfig;

    [SerializeField]
    private Vector3 m_generateOffset = Vector3.zero;

    private void Start()
    {
        GameObject config = GameObject.Find("Config");
        //ConfigManager configManager = config.GetComponent<ConfigManager>();

       // m_bulletConfig = configManager.BulletConfig;
    }

    //behavior.Typeに応じてコンポーネントを付けた弾を生成、自身のtransformの子にする
    //public void GenarateEnemy(ConfigData.BulletConfig.Behaviour behaviour,Vector3 spawnPos)
    //{
    //    GameObject Enemy = GameObject.Instantiate(m_enemyConfig.Base, transform);

    //    Enemy.transform.position = spawnPos + m_generateOffset;
    //    //ボスのポジションにする

    //    Vector3 playerPos = PlayerManager.Instance.Players[0].transform.position;

    //    switch (behaviour.Type)
    //    {
            
    //        case EnemyType.Straight:
    //            StraightMove enemyMove = enemy.AddComponent<StraightMove>();
    //            enemyMove.Initialize(playerPos, behaviour.Speed);
    //            EnemyDestroyer destroyerA = enemy.AddComponent<EnemyDestroyer>();
    //            destroyerA.Initialize(behaviour.Destroy, behaviour.DeadTime);
    //            break;

    //        case EnemyType.Following:
    //            FollowingMove followingMove = enemy.AddComponent<FollowingMove>();
    //            followingMove.Initialize(playerPos, behaviour.Speed);
    //            EnemyDestroyer destroyerB = enemy.AddComponent<EnemyDestroyer>();
    //            destroyerB.Initialize(behaviour.Destroy, behaviour.DeadTime);

    //            break;
    //    }
    }



