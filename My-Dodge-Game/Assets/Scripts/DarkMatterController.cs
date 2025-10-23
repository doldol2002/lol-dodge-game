using UnityEngine;

public class DarkMatterController : MonoBehaviour
{
    [Header("Timings")]
    public float warningTime = 1.5f;
    public float activeTime = 0.2f;

    [Header("References")]
    public GameObject indicator;
    public SpriteRenderer explosionRenderer;
    private CircleCollider2D explosionCollider;

    private float timer;
    private bool hasExploded = false;

    void Start()
    {
        explosionCollider = GetComponent<CircleCollider2D>();
        explosionCollider.enabled = false;
        indicator.transform.localScale = Vector3.zero;

        SoundManager.instance.PlaySound(SoundManager.instance.darkMatterCharge);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < warningTime)
        {
            float scale = timer / warningTime;
            indicator.transform.localScale = new Vector3(scale, scale, 1f);
        }

        if (timer >= warningTime && !hasExploded)
        {
            hasExploded = true;
            indicator.SetActive(false);
            explosionRenderer.enabled = true;
            explosionCollider.enabled = true;

            SoundManager.instance.PlaySound(SoundManager.instance.darkMatterExplode);
        }

        if (timer >=  warningTime + activeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
            Destroy(other.gameObject);
        }
    }
}