using UnityEngine;

public class WaveManager : MonoBehaviour
{
    EnemyGenerator m_generator;

    void Start()
    {
        m_generator = GetComponent<EnemyGenerator>();
        m_generator.GenerateEnemy(Vector3.left);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
