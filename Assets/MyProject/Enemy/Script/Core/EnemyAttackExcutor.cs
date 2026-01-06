using UnityEngine;

public class EnemyAttackExcutor : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Excute(AttackData data)
    {
        GameObject attack = new GameObject("NewAttackObject");
        attack.transform.parent = this.transform;
        attack.transform.position = data.InitPos;
        
        var behaviour = attack.AddComponent<AttackBehaviour>();
        behaviour.Initialize(data);

    }
}
