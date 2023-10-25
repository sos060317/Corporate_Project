using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{

    private Vector3 movePosition;
    private AllyBase ally;

    private bool canMove;

    private void Awake()
    {
        movePosition = transform.position;
        ally = GetComponentInChildren<AllyBase>();
    }

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;

        canMove = true;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        
        Vector3 moveDir = (movePosition - transform.position).normalized;
        
        ally.SetMoveAnimation(true, moveDir);
        
        if (Vector3.Distance(movePosition, transform.position) < 0.3f) 
        {
            moveDir = Vector3.zero; // Stop moving when near
            canMove = false;
            ally.SetMoveAnimation(false, moveDir);
        }
        
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }

}