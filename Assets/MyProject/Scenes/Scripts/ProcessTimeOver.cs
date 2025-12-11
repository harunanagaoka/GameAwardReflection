using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessTimeOver : MonoBehaviour
{
    bool isGameOver = false;
    void Start()
    {

    }
    void Update()
    {
        MainGameTimer mgt = GetComponent<MainGameTimer>();
        if (!isGameOver && mgt.CurrentTime == 0)//CurrentTimeが0以下になったら
        {
            Debug.Log("GameOver");
            isGameOver = true;
        }

        if (isGameOver&& Input.GetButtonDown("Fire4") || isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            // 現在のシーン名を取得
            string currentSceneName = SceneManager.GetActiveScene().name;

            // 同じシーンをロード（再読み込み）
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
