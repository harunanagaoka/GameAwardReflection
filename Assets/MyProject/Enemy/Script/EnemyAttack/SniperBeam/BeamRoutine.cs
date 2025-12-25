using UnityEngine;
using System.Collections;

public class BeamAttackRoutine : MonoBehaviour
{
    [SerializeField] private BeamTelegraphing telegraph;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject beamPrefab;

    [SerializeField] private float trackingTime = 1f;

    void Start()
    {
        StartCoroutine(AttackFlow());
    }

    IEnumerator AttackFlow()
    {
        // ‡@ trackingTime •bŠÔ’Ç”ö
        yield return new WaitForSeconds(trackingTime);

        // ‡A ’Ç”ö’â~i•ûŒüƒƒbƒNj
        telegraph.StopTracking();

        // ‡B ƒƒbƒN‚µ‚½•ûŒü‚ğæ“¾
        Vector3 dir = telegraph.GetLockedDirection();

        // ‡C ƒr[ƒ€¶¬
        GameObject beamObj = Instantiate(beamPrefab, spawnPoint.position, spawnPoint.rotation);

        // ‡D BeamAttack ‚É•ûŒü‚ğ“n‚·
        beamObj.GetComponent<BeamAttack>().Initialize(dir);
    }
}


