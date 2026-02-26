using System.Collections;
using UnityEngine;

public class PhaseCameraChanger : MonoBehaviour
{
    //アニメーション用のfloat名は"PhaseAnimation"

    [SerializeField]
    public Camera cameraB;
    [SerializeField]
    float opentime = 5f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float animationSpeed = 1f;

    private Camera MainCamera;
    private int cameraIndex = 1;
    bool iscorutineRunning = false;
    private float NormalSpeed = 1f;


    void Start()
    {
        MainCamera = Camera.main;
        GameObject pahaseCamera = GameObject.Find("phaseCamera");
        GameObject Enemy = GameObject.Find("boos_anime_taiki_v02");
        animator = Enemy.GetComponent<Animator>();
        cameraB = pahaseCamera.GetComponent<Camera>();
        MainCamera.enabled = true;
        cameraB.enabled = false;
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !iscorutineRunning)
        //{
        //    StartCoroutine(PhaseChanger());
        //    iscorutineRunning = true;
        //}
    }

   public IEnumerator PhaseChanger()
    {
        //スタートで読み込まなかった時用の保険
        //GameObject pahaseCamera = GameObject.Find("phaseCamera");
        //cameraB = pahaseCamera.GetComponent<Camera>();
        //GameObject Enemy = GameObject.Find("boos_anime_taiki_v02");
        //animator = Enemy.GetComponent<Animator>();
        if (cameraIndex == 1)
        {
            // カメラBを有効にし、Canvasを表示
            MainCamera.enabled = false;
            cameraB.enabled = true;
            animator.SetFloat("PhaseAnimation", animationSpeed);    
            cameraIndex = 2;

            yield return new WaitForSeconds(opentime);

            // メインカメラに戻し、Canvasを非表示
            MainCamera.enabled = true;
            cameraB.enabled = false;
            animator.SetFloat("PhaseAnimation", NormalSpeed);
            cameraIndex = 1;
        }
        iscorutineRunning = false;
        yield return null;
    }
}
