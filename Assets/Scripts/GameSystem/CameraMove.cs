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
            if (clickPoint != (Vector2)Input.mousePosition)
            {
                Vector2 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
                
                Vector2 move = position * -(Time.deltaTime * dragSpeed);
                
                transform.Translate(move);
                transform.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                
                if (Input.GetMouseButtonUp(0))
                {
                    //move = position * 0;
                    move = Vector2.zero;
                }
            }
        }
    }
}