using UnityEngine;

[CreateAssetMenu(fileName = "PhaseData", menuName = "Scriptable Objects/PhaseData")]
public class PhaseData : ScriptableObject
{
    [SerializeField]
    private bool m_isSpawnSmallEnemy;
}
