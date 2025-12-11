using UnityEngine;

public class EnemyAttackFactory : ScriptableObject
{
    private AttackData m_attackData;

    public void SetAttackData(AttackData attackData)
    {
        //‰½‚ğƒgƒŠƒK[‚É‚·‚é‚©‚Í–¢’è
        m_attackData = attackData;
    }

    public GameObject CreateAttack(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject attack = new GameObject("NewAttackObject");
        attack.transform.parent = parent;
        attack.transform.position = position;
        attack.transform.rotation = rotation;

        var behaviour = attack.AddComponent<AttackBehaviour>();
        behaviour.Initialize(m_attackData);

        return attack;
    }
}

//AttackPhaseSO‚É‚½‚¹‚éSO
