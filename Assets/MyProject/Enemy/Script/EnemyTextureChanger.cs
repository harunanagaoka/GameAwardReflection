using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTextureChanger : MonoBehaviour
{
    [SerializeField] private PhaseController phaseController;
    [SerializeField] private Material[] phaseMaterials; // フェーズごとのマテリアル
    private List<Renderer> renderers = new List<Renderer>();

    void Awake()
    {
        // ここでは何もしない
    }

    void OnEnable()
    {
        StartCoroutine(FindPhaseControllerWithDelay());
    }

    void OnDisable()
    {
        if (phaseController != null)
            phaseController.OnPhaseChanged -= OnPhaseChangedHandler;
    }

    IEnumerator FindPhaseControllerWithDelay()
    {
        yield return null;

        if (phaseController == null)
            phaseController = Object.FindFirstObjectByType<PhaseController>();

        if (phaseController != null)
            phaseController.OnPhaseChanged += OnPhaseChangedHandler;
    }

    void Start()
    {
        // 子も含めて全てのRendererを取得
        renderers.AddRange(GetComponentsInChildren<Renderer>());
    }

    void OnPhaseChangedHandler(int prevPhase, int nextPhase)
    {
        if (renderers.Count == 0)
        {
            Debug.LogError("Rendererが見つかりません。モデルのパーツ構成を確認してください。");
            return;
        }
        if (phaseMaterials == null || nextPhase >= phaseMaterials.Length)
        {
            Debug.LogError("phaseMaterialsがnull、または配列数が不足しています。");
            return;
        }
        if (phaseMaterials[nextPhase] == null)
        {
            Debug.LogError($"phaseMaterials[{nextPhase}]がnullです。Inspectorで設定してください。");
            return;
        }
        foreach (var renderer in renderers)
        {
            // すべてのマテリアルを一括で差し替え
            var mats = renderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = phaseMaterials[nextPhase];
            }
            renderer.materials = mats;
        }
    }

    private void Update()
    {
        if (phaseController == null)
        {
            StartCoroutine(FindPhaseControllerWithDelay());
        }
    }
}

