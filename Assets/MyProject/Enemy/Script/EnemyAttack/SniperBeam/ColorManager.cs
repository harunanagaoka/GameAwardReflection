using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    private List<ColorKeyData> colorKeys = new List<ColorKeyData>();
    [SerializeField]
    private List<ColorAlphaKeyData> alphaKeys = new List<ColorAlphaKeyData>();

    [SerializeField]
    private bool fadeOut = false;

    private float fade = 1f; // フェード用の透明度
    private LineRenderer lr;

    [System.Serializable]
    public struct ColorKeyData
    {
        public Color color;   // 色
        public float ChangeStartPosition;    // 0〜1 の位置
    }
    [System.Serializable]
    public struct ColorAlphaKeyData
    {
        public float alpha;   // 透明度
        public float ChangeStartPosition;    // 0〜1 の位置
    }



    void Start()
    {
        lr = GetComponent<LineRenderer>();
        //SetupDebugLine();// デバッグ用
    }
    public Gradient CreateGradientWithAlpha(float a)
    {
        Gradient grad = new Gradient();

        // ColorKey はそのまま
        GradientColorKey[] colorKeyArray = new GradientColorKey[colorKeys.Count];
        for (int i = 0; i < colorKeys.Count; i++)
        {
            colorKeyArray[i] = new GradientColorKey(
                colorKeys[i].color,
                colorKeys[i].ChangeStartPosition
            );
        }

        // AlphaKey は全体を a にする
        GradientAlphaKey[] alphaKeyArray = new GradientAlphaKey[2];
        alphaKeyArray[0] = new GradientAlphaKey(a, 0f);
        alphaKeyArray[1] = new GradientAlphaKey(a, 1f);

        grad.SetKeys(colorKeyArray, alphaKeyArray);
        return grad;
    }
    void Update()
    {
        if (fadeOut)
        {
            fade -= Time.deltaTime * 1f; // 1秒で消える
            fade = Mathf.Clamp01(fade);

            Gradient grad = CreateGradientWithAlpha(fade);
            lr.colorGradient = grad;
        }
    }
    private void SetupDebugLine()
    {
        // デバッグ用：LineRenderer に色を適用して確認する
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr == null) return;

        // デバッグ用に最初の状態を表示
        lr.colorGradient = CreateGradientWithAlpha(fade);
        lr.positionCount = 2;
        lr.SetPosition(0, new Vector3(0, 0, 0));
        lr.SetPosition(1, new Vector3(10, 0, 0));
    }


}
