using UnityEngine;

public class SingleHitAttack : MonoBehaviour
{
    public void OnHit()
    {
        Destroy(gameObject);
    }
}
