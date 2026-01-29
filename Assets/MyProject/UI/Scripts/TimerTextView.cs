using UnityEngine;
using TMPro;

public class TimerTextView : MonoBehaviour
{
    [SerializeField]
    private MainGameTimer timer;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Update()
    {
        if (timer == null || timerText == null) return;

        float currentTime = timer.CurrentTime;
        UpdateText(currentTime);
    }

    private void UpdateText(float time)
    {
        if (time < 0f) time = 0f;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{minutes:0}:{seconds:00}";
    }
}

