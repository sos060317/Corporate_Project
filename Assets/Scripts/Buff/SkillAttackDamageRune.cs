using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillAttackDamageRune : EvolutionStoneButton
{
    public override void LevelUp()
    {
        if (buffDetails.buffDatas[curLevel].needGold > GameManager.Instance.currentGold)
        {
            return;
        }
        
        // 효과 적용 로직
        GameManager.Instance.skillAttackDamageMultiply = buffDetails.buffDatas[curLevel].buffForce;
        
        GameManager.Instance.UseGold(buffDetails.buffDatas[curLevel].needGold);
            
        infoText.text = buffDetails.buffDatas[curLevel].buffExplanation;

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
            
            // 버튼 비활성화
            transform.GetComponent<Button>().interactable = false;

            return;
        }
        
        goldText.text = buffDetails.buffDatas[curLevel].needGold.ToString();
    }
}