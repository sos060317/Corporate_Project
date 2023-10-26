using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private GameObject meteorEffectPrefab;
    
    private Vector3 endPos;
    
    public void Init(Vector3 _endPos)
    {
        transform.position = new Vector3(_endPos.x, _endPos.y + 5, _endPos.z);
        
        endPos = _endPos;
    }

    private void Update()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        Vector3 nextPos = Vector3.down * 10 * Time.deltaTime;

        transform.position += nextPos;

        if (Vector3.Distance(transform.position, endPos) < 0.3f)
        {
            Instantiate(meteorEffectPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}