using UnityEngine;

public class DestroyOnAnimeEnd : MonoBehaviour
{
    public void OnAnimEnd()
    {
        Destroy(gameObject);
    }
}
