using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    public Camera cameraA;
    [SerializeField]
    public Camera cameraB;
    [SerializeField]
    public GameObject canvas;
    [SerializeField]
    float opentime = 5f;

    private int cameraIndex = 1;
    bool iscorutineRunning = false;

    void Start()
    {
        canvas.SetActive(false);
        cameraA.enabled = true;
        cameraB.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !iscorutineRunning)
        {
            StartCoroutine(PhaseChanger());
            iscorutineRunning = true;
        }
    }

   public IEnumerator PhaseChanger()
    {
        if (cameraIndex == 1)
        {
            // カメラBを有効にし、Canvasを表示
            cameraA.enabled = false;
            cameraB.enabled = true;
            canvas.SetActive(true);
            cameraIndex = 2;

            yield return new WaitForSeconds(opentime);

            // カメラAに戻し、Canvasを非表示
            cameraA.enabled = true;
            cameraB.enabled = false;
            canvas.SetActive(false);
            cameraIndex = 1;
        }
        iscorutineRunning = false;
        yield return null;
    }
}
