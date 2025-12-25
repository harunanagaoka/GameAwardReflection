using UnityEngine;
public class BeamPierce : MonoBehaviour
{
    [SerializeField] private float beamDistance = 20f;

    private BeamAttack attack;

    void Start()
    {
        attack = GetComponent<BeamAttack>();
    }

    void Update()
    {
        // BeamAttack の transform.forward を使う
        Vector3 dir = attack.transform.forward;

        // RaycastAll（貫通）
        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, beamDistance);

        foreach (var hit in hits)
        {
            // ダメージ処理は他のスクリプトが担当するので何もしない
        }
    }
}

