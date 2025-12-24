using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public enum EnemyAttackType
{
    Random,
    Manual
}

[RequireComponent(typeof(EnemyEvents), typeof(AttackPhaseFactory))]
public class EnemyAttackController : MonoBehaviour
{
    [SerializeField, Tooltip("デバッグ用、手動で攻撃を変えられる")]
    private int m_atkNum = 0;

    [SerializeField, Tooltip("攻撃の選ばれかた")]
    private EnemyAttackType m_attackType = EnemyAttackType.Random;

    private PhaseManager m_phaseManager;

    private PhaseWatcher m_phaseWatcher;

    private AttackPhaseFactory m_attackPhaseFactory;

    private EnemyAttackFactory m_attackFactory;

    private AttackPhaseData m_attackPhaseData;

    [SerializeField]
    private float m_attackInterval = 5f;

    private EnemyEvents m_enemyEvents;

    private void Start()
    {
        m_phaseManager = GetComponent<PhaseManager>();
        m_phaseWatcher = GetComponent<PhaseWatcher>();

        m_enemyEvents = GetComponent<EnemyEvents>();
        m_attackPhaseFactory = GetComponent<AttackPhaseFactory>();
        m_attackFactory = new EnemyAttackFactory();//ふつうにGetComponentでいいかも

        GetCurrentPhaseData();
        m_phaseWatcher.SetCurrentPhaseData(m_attackPhaseFactory.AttackPhaseDatas,m_phaseManager.CurrentPhase);


        // 攻撃開始
        StartCoroutine(AttackLoop());

        m_enemyEvents.OnPhaseChange.AddListener(GetCurrentPhaseData);
    }

    private void GetCurrentPhaseData()
    {
        m_attackPhaseData = m_attackPhaseFactory.GetAtkPhaseData(m_phaseManager.CurrentPhase);
    }

    private IEnumerator AttackLoop()
    {
        //デバッグ用
        while (true)
        {
            yield return new WaitForSeconds(m_attackInterval);

            if (m_atkNum >= m_attackPhaseData.AttackDatas.Length || m_atkNum < 0)
            {
                m_atkNum = 0;
            }

            AttackData attackData = ChoiceAttack();

            //ランダムにする
            m_attackFactory.SetAttackData(attackData);
            m_attackFactory.CreateAttack(attackData.InitPos, transform.rotation, transform);//仮
            m_enemyEvents.OnAttack?.Invoke();
        }
    }

    private AttackData ChoiceAttack()
    {
        AttackData attackData = null;

        switch (m_attackType)
        {
            case EnemyAttackType.Random:

                int randomIndex = Random.Range(0, m_attackPhaseData.AttackDatas.Length);
                attackData = m_attackPhaseData.AttackDatas[randomIndex];

                break;

            case EnemyAttackType.Manual:

                attackData = m_attackPhaseData.AttackDatas[m_atkNum];

                break;
        }

        return attackData;


    }
}


