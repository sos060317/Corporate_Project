using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        Vector3 nextPos = velocityVector * moveSpeed * Time.deltaTime;
        transform.position += nextPos;
    }

    public void Disable()
    {
        this.enabled = false;
        rigidbody2D.velocity = Vector3.zero;
    }

    public void Enable()
    {
        this.enabled = true;
    }
}
