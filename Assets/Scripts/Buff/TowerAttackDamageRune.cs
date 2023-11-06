using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerAttackDamageRune : EvolutionStoneButton
{ 
    public override void LevelUp()
    {
        if (buffDetails.buffDatas[curLevel].needGold > GameManager.Instance.currentGold)
        {
            return;
        }
        
        // 효과 적용 로직
        GameManager.Instance.allyAttackDamageMultiply = buffDetails.buffDatas[curLevel].buffForce;
        
        GameManager.Instance.UseGold(buffDetails.buffDatas[curLevel].needGold);
        
        infoText.text = buffDetails.buffDatas[curLevel].buffExplanation;

        curLevel++;
        
        // 아직 만랩이 아니라면
        if (curLevel < buffDetails.buffDatas.Length)
        {
            Instantiate(levelStarPrefab, levelStarParent);

            if (icon == null)
            {
                icon = Instantiate(iconPrefab, GameManager.Instance.buffIconParent);
            }
            
            icon.TextChange(curLevel.ToString());
        }
        // 만약 만랩이라면
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