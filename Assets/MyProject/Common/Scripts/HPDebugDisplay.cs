using UnityEngine;
using System.Collections.Generic;

public class HPDebugDisplay : MonoBehaviour
{
    [SerializeField]
    [Tooltip("HP表示を更新する間隔（秒）")]
    private float m_updateInterval = 0.5f;

    [SerializeField]
    [Tooltip("フォントサイズ")]
    private int m_fontsize = 14;

    private float m_timer = 0;

    private List<PlayerDamageable> m_players = new List<PlayerDamageable>();
    private List<EnemyDamageable> m_enemies = new List<EnemyDamageable>();

    void Start()
    {
        RefreshDamageableList();
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_updateInterval)
        {
            RefreshDamageableList();
            m_timer = 0;
        }
    }

    private void RefreshDamageableList()
    {
        m_players.Clear();
        m_enemies.Clear();

        // シーン上の全てのPlayerDamageableを取得
        m_players.AddRange(FindObjectsByType<PlayerDamageable>(FindObjectsSortMode.None));

        // シーン上の全てのEnemyDamageableを取得
        m_enemies.AddRange(FindObjectsByType<EnemyDamageable>(FindObjectsSortMode.None));
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));

        GUILayout.Label("=== HP Debug Display ===", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize + 2, fontStyle = FontStyle.Bold });

        // プレイヤーのHP表示
        GUILayout.Space(10);
        GUILayout.Label("[ Players ]", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize, fontStyle = FontStyle.Bold });
        if (m_players.Count == 0)
        {
            GUILayout.Label("  No Players Found", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize });
        }
        else
        {
            foreach (var player in m_players)
            {
                if (player != null)
                {
                    string playerName = player.gameObject.name;
                    int hp = GetPlayerHP(player);
                    GUILayout.Label($"  {playerName}: HP = {hp}", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize });
                }
            }
        }

        // 敵のHP表示
        GUILayout.Space(10);
        GUILayout.Label("[ Enemies ]", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize, fontStyle = FontStyle.Bold });
        if (m_enemies.Count == 0)
        {
            GUILayout.Label("  No Enemies Found", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize });
        }
        else
        {
            foreach (var enemy in m_enemies)
            {
                if (enemy != null)
                {
                    string enemyName = enemy.gameObject.name;
                    int hp = GetEnemyHP(enemy);
                    GUILayout.Label($"  {enemyName}: HP = {hp}", new GUIStyle(GUI.skin.label) { fontSize = m_fontsize });
                }
            }
        }

        GUILayout.EndArea();
    }

    // リフレクションでprivateなm_hitPointを取得
    private int GetPlayerHP(PlayerDamageable player)
    {
        var field = typeof(PlayerDamageable).GetField("m_hitPoint",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (field != null)
        {
            return (int)field.GetValue(player);
        }
        return -1; // 取得失敗
    }

    private int GetEnemyHP(EnemyDamageable enemy)
    {
        var field = typeof(EnemyDamageable).GetField("m_hitPoint",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (field != null)
        {
            return (int)field.GetValue(enemy);
        }
        return -1; // 取得失敗
    }
}