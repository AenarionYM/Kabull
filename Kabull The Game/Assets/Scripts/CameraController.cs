using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Objects
    public Transform player;
    public Vector3 cameraOffset;
    
    //Variables
    public float cameraSpeed = 0.1f;
    
    private void Start()
    {
        transform.position = player.position + cameraOffset;
    }

    void FixedUpdate()
    {
        Vector3 finalPosition = player.position + cameraOffset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, finalPosition, cameraSpeed);
        transform.position = lerpPosition;
    }
}
