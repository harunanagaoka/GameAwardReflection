using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public void StartMove(MoveTimelineData.MoveStepData data, Transform target)
    {
        StartCoroutine(Move(data, target));
    }

    private IEnumerator Move(MoveTimelineData.MoveStepData data,Transform target)
    {
        Transform moveTarget = target;
        Vector3 startPos = target.position;
        float distance = (data.TargetPos - startPos).magnitude;
        float elapsed = 0f;
        float totalMoveAmount = 0f;
        while (elapsed < data.MoveDuration)
        {
            elapsed += Time.fixedDeltaTime;

            if (totalMoveAmount > distance)
            {
                target.position = data.TargetPos;
                yield return new WaitForFixedUpdate();
                continue;
            }

            Vector3 dir = data.TargetPos - startPos;
            Vector3 dist = dir.normalized * data.Velocity * Time.fixedDeltaTime;
            target.position += dist;
            totalMoveAmount += dist.magnitude;
            yield return new WaitForFixedUpdate();
        }
        target.position = data.TargetPos;
    }
    //ŽžŠÔ“à‚É‚»‚±‚É‹ß‚Ã‚­
}
