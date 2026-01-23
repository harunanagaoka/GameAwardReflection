using UnityEngine;

public class GuardRecoveryItem : MonoBehaviour
{
    
[SerializeField]
    private float m_recoveryValue = 10f;

    private void OnTriggerEnter(Collider other) 
    { 
        if (other.TryGetComponent<PlayerDefenceGauge>(out PlayerDefenceGauge defenceGauge))
        {
            defenceGauge.DecreceDefenceGauge(m_recoveryValue);
            Destroy(this.gameObject);
        }
    }
}
