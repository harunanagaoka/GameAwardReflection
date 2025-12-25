using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    [SerializeField] private float beamDistance = 20f;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private BoxCollider boxCol;
    [SerializeField] private ColorManager colorManager;
    [SerializeField] private GameObject Player;

    private Vector3 direction;
    private bool isInitialized = false;

    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
        isInitialized = true;
    }

    void Start()
    {
        Player = PlayerManager.Instance.Players[0];
        lr = GetComponentInChildren<LineRenderer>();

        // ColorManager からグラデーションをもらう
        lr.colorGradient = colorManager.CreateGradientWithAlpha(1f);
    }

    void Update()
    {
    //    if (!isInitialized) return;

        // Raycast（非貫通）
        Physics.Raycast(transform.position, direction, out _, beamDistance);

        // LineRenderer（常に表示）
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);// 開始位置
        lr.SetPosition(1, Player.transform.position /*+ direction * beamDistance*/);// 終了位置

        // コライダーの長さを BeamDistance に合わせる
        boxCol.size = new Vector3(boxCol.size.x, boxCol.size.y, beamDistance); 
        // コライダーの中心を前方に移動させる
        boxCol.center = new Vector3(0, 0, beamDistance / 2f);
    }
}
