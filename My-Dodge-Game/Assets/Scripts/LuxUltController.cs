using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LuxUltController : MonoBehaviour
{
    [Header("Timings")]
    public float warningTime = 2.0f;
    public float activeTime = 0.5f;

    [Header("References")]
    public GameObject warningLine;
    public GameObject beam;

    [Header("Audio")]
    public AudioClip warningSound;

    [Range(0f, 1f)]
    public float fireSoundVolume = 0.7f;

    private AudioSource audioSource;
    private bool hasFired = false;
    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        beam.SetActive(false);

        if (warningSound != null)
        {
            audioSource.clip = warningSound;
            audioSource.Play();
        }

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= warningTime && !hasFired)
        {
            hasFired = true;

            audioSource.Stop();

            warningLine.SetActive(false);
            beam.SetActive(true);

            SoundManager.instance.PlaySound(SoundManager.instance.luxFire, fireSoundVolume);
        }

        if (timer>= warningTime + activeTime)
        {
            Destroy(gameObject);
        }
    }
}