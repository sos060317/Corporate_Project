using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldBanana : MonoBehaviour
{
    public GameObject banana;
    public GoldLevel goldLevel;

    [SerializeField]
    private float freshnessTime;
    [SerializeField]
    private float spoilingTime;

    private bool isSpoiled = false;
    private SpriteRenderer spriteRenderer;
    private Tween moveTween;
    private int bananaLevel;

    public void InitializeLevel(int level, GoldLevel goldLevel)
    {
        bananaLevel = level;
        this.goldLevel = goldLevel;
    }


    private void Start()
    {
        moveTween = transform.DOLocalMoveY(-2f, 1.5f).SetRelative();

        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SpoilOverTime());
    }

    private void Update()
    {

    }

    IEnumerator SpoilOverTime()
    {
        yield return new WaitForSeconds(freshnessTime);

        while (spoilingTime > 0f)
        {
            yield return null;
            spoilingTime -= Time.deltaTime;

            if (spoilingTime <= 0f && !isSpoiled)
            {
                isSpoiled = true;
                SpoiledBanana();
            }
            else
            {
                //색 어둡게
                float lerpFactor = 1f - spoilingTime / freshnessTime;
                Color newColor = Color.Lerp(Color.white, Color.black, lerpFactor);
                spriteRenderer.color = newColor;
            }
        }
    }

    private void SpoiledBanana()
    {
        Destroy(gameObject);
    }

    public void GetGold()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            return;
        }
        else
        {
            GameManager.Instance.GetGold(goldLevel.goldTowerLevel[bananaLevel].amountGold);
            Debug.Log(goldLevel.goldTowerLevel[bananaLevel].amountGold + "의 Gold를 얻었습니다!");
            Destroy(gameObject);
        }
    }

}
