using UnityEngine;

public class EnemyAttackFactory : ScriptableObject
{
    private AttackPhaseData m_attackDataList;

    public void SetAttackPhaseData(AttackPhaseData attackDataList)
    {
        //‰½‚ğƒgƒŠƒK[‚É‚·‚é‚©‚Í–¢’è
        m_attackDataList = attackDataList;
    }

    public GameObject CreateAttack(int AtkNum, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject attack = new GameObject("NewAttackObject");
        attack.transform.parent = parent;
        attack.transform.position = position;
        attack.transform.rotation = rotation;

        var behaviour = attack.AddComponent<AttackBehaviour>();
        behaviour.Initialize(m_attackDataList.AttackDatas[AtkNum]);

        return attack;
    }
}

//AttackPhaseSO‚É‚½‚¹‚éSO
