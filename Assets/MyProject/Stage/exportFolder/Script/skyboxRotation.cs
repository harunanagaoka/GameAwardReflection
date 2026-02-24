using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class skyboxRotation : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float rotationSpeed;
    [SerializeField] private Material skyboxMaterial;
    private float rotationRepeatValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationRepeatValue = Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotationSpeed, 360f);

        skyboxMaterial.SetFloat("_Rotation", rotationRepeatValue);
    }
}
