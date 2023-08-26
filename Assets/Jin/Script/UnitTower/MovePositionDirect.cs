using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{

    private Vector3 movePosition;
    private AllyBase ally;

    private void Awake()
    {
        movePosition = transform.position;
        ally = GetComponentInChildren<AllyBase>();
    }

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Update()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        
        ally.SetMoveAnimation(true, moveDir);
        if (Vector3.Distance(movePosition, transform.position) < 1f) 
        {
            moveDir = Vector3.zero; // Stop moving when near
            ally.SetMoveAnimation(false, moveDir);
        }
        
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }

}