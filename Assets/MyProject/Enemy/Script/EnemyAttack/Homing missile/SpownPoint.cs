using UnityEngine;

public class SpownPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject Boss;
    [SerializeField]
    private GameObject Missile;

    void Start()
    {
        Missile.transform.position = Boss.transform.position;
    }

}
