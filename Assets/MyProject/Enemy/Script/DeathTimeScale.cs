using System.Collections;
using UnityEngine;

public class DeathTimeScale : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ヒットストップの持続時間")]
    private float hitStopDuration = 0.5f; 

    [SerializeField]
    [Tooltip("ヒットストップ中の時間のスケール")]
    private float hitStopSlowTimeScale = 0.1f;

    [SerializeField]
    private GameObject effectObject;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeathDelay();
        }
    }


    public void DeathDelay()
    {
        animator.SetBool("timeScale", true); 
        effectObject.SetActive(true);
    }

}
