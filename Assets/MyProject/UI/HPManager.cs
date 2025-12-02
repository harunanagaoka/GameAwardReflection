using System.Collections;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    // Inspectorで設定する（GaugeController側には表示しない）
    [SerializeField] private GameObject _gauge;
    [SerializeField] private GameObject _graceGauge;
    [SerializeField] private int _HP = 100;
    [SerializeField] private float _waitingTime = 0.5f;

    // 計算用
    private float _HP1;

    private void Awake()
    {
        if (_gauge == null)
        {
            Debug.LogError("HPManager: _gauge がアサインされていません。Inspectorで設定してください。");
            return;
        }
        if (_HP <= 0)
        {
            Debug.LogError("HPManager: _HP が 0 以下です。正の値を設定してください。");
            return;
        }

        _HP1 = _gauge.GetComponent<RectTransform>().sizeDelta.x / _HP;
    }

    // 外部から呼べる公開メソッド
    public void ApplyDamage(int attack)
    {
        float damage = _HP1 * attack;
        StartCoroutine(DamageCoroutine(damage));
    }

    // 体力ゲージを減らすコルーチン
    private IEnumerator DamageCoroutine(float damage)
    {
        if (_gauge == null)
            yield break;

        Vector2 now = _gauge.GetComponent<RectTransform>().sizeDelta;
        now.x -= damage;
        if (now.x < 0f) now.x = 0f;
        _gauge.GetComponent<RectTransform>().sizeDelta = now;

        yield return new WaitForSeconds(_waitingTime);

        if (_graceGauge != null)
            _graceGauge.GetComponent<RectTransform>().sizeDelta = now;
    }
}
