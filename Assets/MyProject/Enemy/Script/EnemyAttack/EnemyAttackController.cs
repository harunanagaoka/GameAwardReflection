using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    EnemyData m_enemyData;
    EnemyEvents m_enemyEvents;//使わないかも。
    EnemyAttackFactory m_factory;//初期化

    private List<Coroutine> m_runningTasks = new();

    public void Initialize(EnemyData data,EnemyEvents events,EnemyAttackFactory factory)
    {
        m_enemyData = data;
        m_factory = factory;
        m_enemyEvents = events;
    }

    public void OnPhaseChanged(int phase)
    {
        StopAllTasks();

        var timelineDatas = m_enemyData.AttackPetternData.AttackTaskDatas[phase];//Timelineを抜き出す

        StartTasks(timelineDatas);
    }

    //攻撃の生成を開始する
    private void StartTasks(AttackTimelineData timelineDatas)
    {
        foreach (var timeline in timelineDatas.Timelines)
        {
            var c = StartCoroutine(RunAttackTimeline(timeline));
            m_runningTasks.Add(c);
        }
    }


    private void StopAllTasks()
    {
        foreach (var c in m_runningTasks)
        {
            if (c != null)
            {
                StopCoroutine(c);
            }

        }
        m_runningTasks.Clear();
    }

    private IEnumerator RunAttackTimeline(AttackTimelineData.AttackTimeline timeline)
    {
        int index = 0;

        while (true)
        {
            yield return new WaitForSeconds(timeline.Interval);

            var attack = timeline.StepDatas[index].AttackData;
            ExecuteAttack(attack);

            index = (index + 1) % timeline.StepDatas.Length;
        }
    }

    private void ExecuteAttack(AttackData data)
    {
        m_factory.CreateAttack(data, this.transform);
    }

}
