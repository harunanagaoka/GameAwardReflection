using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [SerializeField]
    MainGameTimer mainGameTimer;
    [SerializeField] 
    TextMeshProUGUI timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currenttime = mainGameTimer.CurrentTime;

        int minutes = Mathf.FloorToInt(currenttime / 60);
        int seconds = Mathf.FloorToInt(currenttime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
       // timeBarText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
