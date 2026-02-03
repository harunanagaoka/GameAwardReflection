using UnityEngine;

public class BG_Scroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed_x;
    [SerializeField] private float scrollSpeed_y;
    [SerializeField] private float scrollSpeed_z;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(scrollSpeed_x * Time.deltaTime, 0, 0);
        transform.rotation *= Quaternion.Euler(0, scrollSpeed_y * Time.deltaTime, 0);
        transform.rotation *= Quaternion.Euler(0, 0, scrollSpeed_z * Time.deltaTime);

    }
}
