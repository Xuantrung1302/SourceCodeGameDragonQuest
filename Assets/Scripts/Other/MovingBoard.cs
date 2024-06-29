using UnityEngine;

public class MovingBoard : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject pos1, pos2;
    private Vector2 target;

    private bool isMoving = true; // Biến để kiểm tra MovingBoard có đang hoạt động hay không

    void Start()
    {
        target = pos1.transform.position;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            if ((Vector2)transform.position == (Vector2)pos1.transform.position)
            {
                target = pos2.transform.position;
            }
            else if ((Vector2)transform.position == (Vector2)pos2.transform.position)
            {
                target = pos1.transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            UnsetPlayerParent(collision.collider.transform);
        }
    }

    private void UnsetPlayerParent(Transform playerTransform)
    {
        // Kiểm tra xem MovingBoard có đang active hay không
        if (gameObject.activeInHierarchy)
        {
            playerTransform.SetParent(null);
        }
    }

    private void OnDisable()
    {
        // Gọi UnsetPlayerParent cho tất cả các player đang ở trên MovingBoard khi nó bị vô hiệu hóa
        var playersOnBoard = GetComponentsInChildren<Transform>();
        foreach (var playerTransform in playersOnBoard)
        {
            if (playerTransform.CompareTag("Player"))
            {
                UnsetPlayerParent(playerTransform);
            }
        }

        isMoving = false; // Đặt biến isMoving về false khi MovingBoard bị vô hiệu hóa
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1.transform.position, pos2.transform.position);
    }
}
