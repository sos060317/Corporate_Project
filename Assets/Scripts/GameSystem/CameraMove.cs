using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 30.0f;
    private Vector2 clickPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

            Vector2 move = position * -(Time.deltaTime * dragSpeed);

            transform.Translate(move);
            transform.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}