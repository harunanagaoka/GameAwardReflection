using System.Net;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    EnemyGenerator m_generator;

    private int m_currentEnemyCount = 0;

    private bool m_isCleared = false;

    public bool IsCleared => m_isCleared;

    void Start()
    {
        m_generator = GetComponent<EnemyGenerator>();
        //m_generator.GenerateEnemy(Vector3.left);
    }


    void Update()
    {
        m_isCleared = CheckWaveClear();
    }

    private bool CheckWaveClear()
    {
        if(transform.childCount <= 0)
        {
            return true;
        }

        return false;
    }
}
