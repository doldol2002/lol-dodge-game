using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition.z = 0f;

        transform.position = mouseWorldPosition;
    }
}


