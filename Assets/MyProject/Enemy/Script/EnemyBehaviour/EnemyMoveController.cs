using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    EnemyData m_enemyData;
    EnemyMover m_mover;

    private List<Coroutine> m_runningTasks = new();

    public void Initialize(EnemyData data, EnemyMover mover)
    {
        m_enemyData = data;
        m_mover = mover;
    }

    public void OnPhaseChanged(int phase)
    {
        StopAllTasks();

        var timelineDatas = m_enemyData.MovePetternData.MoveTaskDatas[phase];

        StartTasks(timelineDatas);
    }

    //ˆÚ“®‚ðŠJŽn‚·‚é
    private void StartTasks(MoveTimelineData timelineDatas)
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

    private IEnumerator RunAttackTimeline(MoveTimelineData.MoveTimeline timeline)
    {
        int index = 0;

        while (true)
        {
            var move = timeline.StepDatas[index];
            ExecuteMove(move);
            yield return new WaitForSeconds(timeline.StepDatas[index].MoveDuration);

            index = (index + 1) % timeline.StepDatas.Length;
        }
    }

    private void ExecuteMove(MoveTimelineData.MoveStepData data)
    {
        m_mover.StartMove(data, this.transform);
    }
}
