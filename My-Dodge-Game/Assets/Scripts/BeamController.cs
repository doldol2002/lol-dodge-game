using UnityEngine;

public class BeamController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();

            Destroy(other.gameObject);
        }
    }
}
