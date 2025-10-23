using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Sound Effect Clips")]
    public AudioClip mysticShotFire;
    public AudioClip darkMatterCharge;
    public AudioClip darkMatterExplode;
    public AudioClip playerHit;
    public AudioClip gameStart;
    public AudioClip luxFire;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }
}