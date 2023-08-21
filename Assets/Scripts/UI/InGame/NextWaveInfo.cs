using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveInfo : MonoBehaviour
{
    [SerializeField] private Text infoText;

    public void InitInfo(List<string> enemyNameList, List<int> enemyCountList)
    {   
        infoText.text = "정보\n\n";

        for (int i = 0; i < enemyNameList.Count; i++)
        {
            infoText.text += enemyNameList[i] + " X " + enemyCountList[i].ToString();
            
            if (i - enemyNameList.Count > 0)
            {
                infoText.text += "\n";
            }
        }
    }
}