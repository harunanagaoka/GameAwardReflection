using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField]
    private int m_playerCount = 0;

    [SerializeField]
    private GameObject m_playerPrefab;
   
    void Awake()
    {
        PlayerManager.Instance.ResetManager();
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        for (int i = 0; i < m_playerCount; i++)
        {
            GameObject player = GameObject.Instantiate(m_playerPrefab);
            PlayerManager.Instance.RegisterPlayer(player);
        }
    }
}
