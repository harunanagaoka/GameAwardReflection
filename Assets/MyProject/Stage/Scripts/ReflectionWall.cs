using UnityEngine;

public class ReflectionWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerBlownAway player = collision.gameObject.GetComponent<PlayerBlownAway>();
            if (player != null)
            {
                Vector3 normal = collision.contacts[0].normal;
                player.Reflect(normal);
            }
        }
    }
}
