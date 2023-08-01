using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;

    private float tempValue;

    private Camera camera;

    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (camera.orthographicSize <= 2.67f && scroll > 0)
        {
            tempValue = camera.orthographicSize;
            camera.orthographicSize = tempValue;
        }
        else if (camera.orthographicSize >= 7.03f && scroll < 0)
        {
            tempValue = camera.orthographicSize;
            camera.orthographicSize = tempValue;
        }
        else
        {
            camera.orthographicSize -= scroll * 0.5f;
        }
    }
}
