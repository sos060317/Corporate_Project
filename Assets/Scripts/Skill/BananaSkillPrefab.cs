using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BananaSkillPrefab : MonoBehaviour
{
    [SerializeField] private int bananaCount;
    
    [SerializeField] private Banana bananaPrefab;
    [SerializeField] private SpriteRenderer sizeObj;

    [SerializeField] private Color ableColor;
    [SerializeField] private Color unableColor;

    private bool attackPossible = false;
    
    private Vector2 skillPos;

    private bool stop;

    private void Update()
    {
        if (stop)
        {
            return;
        }
        
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            -Camera.main.transform.position.z));

        if (Input.GetMouseButtonDown(0) && attackPossible)
        {
            sizeObj.gameObject.SetActive(false);

            skillPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                -Camera.main.transform.position.z));
            
            Debug.Log(skillPos);

            stop = true;

            StartCoroutine(MeteorRoutine());
        }
    }
    
    private IEnumerator MeteorRoutine()
    {
        for (int i = 0; i < bananaCount; i++)
        {
            var meteor = Instantiate(bananaPrefab, skillPos, Quaternion.identity);
            
            meteor.Init(skillPos + Random.insideUnitCircle);
            
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Road") || other.CompareTag("Enemy"))
        {
            sizeObj.color = ableColor;
            attackPossible = true;
        }
        else
        {
            sizeObj.color = unableColor;
            attackPossible = false;
        }
    }
}
