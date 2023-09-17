using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDamageRune : EvolutionStoneButton
{
    protected override void LevelUp()
    {
        // 효과 적용 로직
        GameManager.Instance.enemyAttackDamageMultiply = buffDetails.buffDatas[curLevel].buffForce;
        
        GameManager.Instance.UseGold(buffDetails.buffDatas[curLevel].needGold);
        
        infoText.text = buffDetails.buffDatas[curLevel].buffExplanation;

        levelUpEffect.Play();

        curLevel++;

        if (curLevel < buffDetails.buffDatas.Length)
        {
            Instantiate(levelStarPrefab, levelStarParent);

            if (icon == null)
            {
                icon = Instantiate(iconPrefab, GameManager.Instance.buffIconParent);
            }
            
            icon.TextChange(curLevel.ToString());
        }
        else
        {
            levelStarParent.gameObject.SetActive(false);
            levelMaxText.SetActive(true);
            goldText.text = "";
            icon.TextChange("MAX");
        }
    }
}