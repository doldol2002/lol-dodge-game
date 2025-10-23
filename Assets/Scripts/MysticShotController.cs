using UnityEngine;

public class MysticShotController : MonoBehaviour
{
    public float speed;

    private Vector2 direction;
    private Camera mainCamera;
    private float despawnBuffer = 1.5f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        Vector2 screenPos = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.x < 0 - despawnBuffer || screenPos.x > 1 + despawnBuffer || screenPos.y <0 - despawnBuffer || screenPos.y > 1 + despawnBuffer)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirectionAndSpeed(Vector2 newDirection, float newSpeed)
    {
        direction = newDirection;
        speed = newSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}