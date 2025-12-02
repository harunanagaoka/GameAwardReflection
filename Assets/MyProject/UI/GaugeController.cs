using UnityEngine;

public class GaugeController : MonoBehaviour
{
    // HPの実ロジック・フィールドは HPManager にまとめる
    private HPManager _hpManager;

    private void Awake()
    {
        // 同じ GameObject に HPManager を置く想定
        _hpManager = GetComponent<HPManager>();
        if (_hpManager == null)
        {
            Debug.LogError("GaugeController: HPManager が見つかりません。同じ GameObject にアタッチするか Inspector 参照を渡してください。");
        }
    }

    // 外部（ボタン等）から呼ぶインターフェース
    public void BeInjured(int attack)
    {
        if (_hpManager == null) return;
        _hpManager.ApplyDamage(attack);
    }
}