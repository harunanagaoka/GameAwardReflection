using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    EnemyData m_enemyData;

    private List<Coroutine> m_runningTasks = new();
    public void OnPhaseChanged(int phase)
    {
        StopAllTasks();

        var sequenceData = m_enemyData.AttackPetternData.AttackTaskDatas[phase].StepDatas;

        StartTasks(sequenceData);
    }

    //順番にひとつずつ生成する
    private void StartTasks(AttackTimelineData.AttackStepData[] steps)
    {
            foreach (var step in steps)
        {
            if (step.AttackData == null) continue;

            var c = StartCoroutine(RunAttackTask(step));
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

    //private IEnumerator StartTimeline(AttackTimelineData.AttackTimeline[] timelines)
    //{
    //    //のちにフェーズ内で攻撃パターン切り替えもできるようにする
    //}

    private IEnumerator RunAttackTimeline(AttackTimelineData.AttackStepData[] steps)
    {
        int index = 0;

        while (true)
        {
            yield return new WaitForSeconds(steps[index].Interval);

            var attack = steps[index].AttackData;
            ExecuteAttack(attack);

            index = (index + 1) % steps.Length;
        }
    }

    private IEnumerator RunAttackTask(AttackTimelineData.AttackStepData steps)
    {
        while (true)
        {
            yield return new WaitForSeconds(steps.Interval);

            var attack = steps.AttackData;
            ExecuteAttack(attack);
        }
    }

    private void ExecuteAttack(AttackData data)
    {
        // AttackFactoryに投げる
    }

}
