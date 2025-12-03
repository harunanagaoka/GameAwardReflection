using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyEvents),typeof(AttackPhaseFactory))]
public class EnemyAttackController : MonoBehaviour
{
    [SerializeField, Tooltip("デバッグ用、手動でフェーズを変えられる")]
    private GamePhase m_phase = GamePhase.Phase1;//将来的にはphase管理クラスから取得する

    [SerializeField, Tooltip("デバッグ用、手動で攻撃を変えられる")]
    private int m_atkNum = 0;

    private AttackPhaseFactory m_attackPhaseFactory;

    private EnemyAttackFactory m_attackFactory;

    private AttackPhaseData m_attackPhaseData; 

    [SerializeField]
    private float m_attackInterval = 5f; 

    private EnemyEvents m_enemyEvents;

    private void Start()
    {
        m_enemyEvents = GetComponent<EnemyEvents>();
        m_attackPhaseFactory = GetComponent<AttackPhaseFactory>();
        m_attackFactory = new EnemyAttackFactory();

        GetCurrentPhaseData();
        m_attackFactory.SetAttackPhaseData(m_attackPhaseData);

        // 攻撃開始
        StartCoroutine(AttackLoop());
    }

    private void GetCurrentPhaseData()
    {
        //フェーズを全体で指定するのか敵ごとに指定するのかわからないため現在はこのスクリプトでフェーズを指定
        m_attackPhaseData = m_attackPhaseFactory.GetAtkPhaseData(m_phase);
    }

    private IEnumerator AttackLoop()
    {
        //デバッグ用
        while (true)
        {
            yield return new WaitForSeconds(m_attackInterval);

            if(m_atkNum >= m_attackPhaseData.AttackDatas.Length || m_atkNum < 0)
            {
                m_atkNum = 0;
            }

            m_attackFactory.CreateAttack(m_atkNum, transform.position, transform.rotation, transform);//仮
            m_enemyEvents.OnAttack?.Invoke();
        }
    }
}
