using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public float destroyTime;
    
    private Vector3 endPos;

    private bool ableBanana = false;
    
    public void Init(Vector3 _endPos)
    {
        transform.position = new Vector3(_endPos.x, _endPos.y + 5, _endPos.z);
        
        endPos = _endPos;

        Invoke(nameof(Destroy), destroyTime);
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            Vector3 nextPos = Vector3.down * 10 * Time.deltaTime;

            transform.position += nextPos;

            if (Vector3.Distance(transform.position, endPos) < 0.3f)
            {
                ableBanana = true;
                
                break;
            }

            yield return null;
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && ableBanana)
        {
            other.GetComponent<EnemyBase>().FaintEnemy(2);
            
            Destroy(gameObject);
        }
    }
}