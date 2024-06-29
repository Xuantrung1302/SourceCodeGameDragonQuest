using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    private Vector3 offset = new Vector3(3f, 0f, -10f);
    private float smoothTime = 0.25f;

    [SerializeField] private Transform target;
    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), 
        //    ref velocity, speed);
        
        //Follow player
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

}
