using UnityEngine;

public class EnemyAttackFactory : MonoBehaviour
{
    public GameObject CreateAttack(AttackData data ,Transform parent)
    {
        GameObject attack = new GameObject("NewAttackObject");
        attack.transform.parent = parent;

        var behaviour = attack.AddComponent<AttackBehaviour>();
        behaviour.Initialize(data);

        return attack;
    }
}
