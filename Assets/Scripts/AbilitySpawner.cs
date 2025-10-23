using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    [Header("Prefabs")]

    public GameObject mysticShotPrefab;
    public GameObject darkMatterPrefab;
    public GameObject luxUltPrefab;

    [Header("Target")]
    public Transform playerTransform;

    [Header("Spawn Rate Scaling")]
    public float initialSpawnRate = 0.3f;
    public float spawnRateIncrease = 0.05f;

    [Header("Projectile Speed Scaling")]

    public float initialProjectileSpeed = 3f;
    public float projectileSpeedIncrease = 0.2f;

    private float currentSpawnRate;
    private float currentProjectileSpeed;
    private float mysticShotTimer;
    private float darkMatterTimer;
    private float luxUltTimer;

    private Camera mainCamera;
    private float screenBuffer = 1.1f;

    void Start()
    {
        mainCamera = Camera.main;

        currentSpawnRate = initialSpawnRate;
        currentProjectileSpeed = initialProjectileSpeed;

        mysticShotTimer = 1f / currentSpawnRate;
        darkMatterTimer = 2.5f / currentSpawnRate;
        luxUltTimer = 5f;
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        currentSpawnRate += spawnRateIncrease * Time.deltaTime;
        currentProjectileSpeed += projectileSpeedIncrease * Time.deltaTime;

        mysticShotTimer -= Time.deltaTime;
        if (mysticShotTimer <= 0)
        {
            SpawnMysticShot();
            mysticShotTimer = 1f / currentSpawnRate;
        }

        darkMatterTimer -= Time.deltaTime;
        if (darkMatterTimer <= 0)
        {
            SpawnDarkMatter();
            darkMatterTimer = 2.5f / currentSpawnRate;
        }

        luxUltTimer -= Time.deltaTime;
        if (luxUltTimer <= 0)
        {
            SpawnLuxUlt();
            luxUltTimer = 5f;
        }
    }

    void SpawnLuxUlt()
    {
        Vector2 spawnPoint = Vector2.zero;
        Quaternion rotation = Quaternion.identity;

        if (Random.value > 0.5f)
        {
            float randomY = Random.Range(0.1f, 0.9f);
            spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, randomY));
        }
        else
        {
            float randomX = Random.Range(0.1f, 0.9f);
            spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(randomX, 0.5f));
            rotation = Quaternion.Euler(0, 0, 90);
        }

        Instantiate(luxUltPrefab, spawnPoint, rotation);
    }
        
    void SpawnMysticShot()
    {
        int side = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(Random.value, 0 - screenBuffer));
                break;
            case 1:
                spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(Random.value, 1 + screenBuffer));
                break;
            case 2:
                spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(0 - screenBuffer, Random.value));
                break;
            case 3:
                spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(1 + screenBuffer, Random.value));
                break;
        }

        GameObject shot = Instantiate(mysticShotPrefab, spawnPoint, Quaternion.identity);

        Vector2 direction = ((Vector2)playerTransform.position - spawnPoint).normalized;

        shot.GetComponent<MysticShotController>().SetDirectionAndSpeed(direction, currentProjectileSpeed);

        SoundManager.instance.PlaySound(SoundManager.instance.mysticShotFire);
    }

    void SpawnDarkMatter()
    {
        float randomX = Random.Range(0.1f, 0.9f);
        float randomY = Random.Range(0.1f, 0.9f);
        Vector2 spawnPoint = mainCamera.ViewportToWorldPoint(new Vector2(randomX, randomY));

        Instantiate(darkMatterPrefab, spawnPoint, Quaternion.identity);
    }
}