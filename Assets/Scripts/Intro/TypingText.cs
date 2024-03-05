using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class TypingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private string[] typingText = { "이곳은 동물의 왕국\r\n" +
                                    "모든 종족들은 자신들의 능력을 가지고 있다\r\n" +
                                    "하지만 유일하게 원숭이 종족만 능력을 찾지 못하고 있었는데...",

                                    "이곳은 약육강식인 동물의 왕국\r\n" +
                                    "다른 종족들은 그런 원숭이 종족을\r\n" +
                                    "가만히 놔두질 않았다...",

                                    "그러던 어느날...",

                                    "원숭이 종족은 자신들의 왕국 깊숙한 곳에\r\n" +
                                    "원숭이족의 능력, \"진화\"를 찾게 되었다",

                                    "그리하여 원숭이 종족은 자신들의 \"타워\"를 업그레이드 할 수 있게 되었고",

                                    "그리하여...\r\n" +
                                    "그 능력으로 원숭이 종족은 다른 종족들에게\r\n" +
                                    "빼앗긴 자신의 영토를 되찾기 위해서 싸움을 걸게 되는데...."
    };

    private string introPlayerPrefsKey = "HasSeenIntro";

    private int currentTypingNumber = 0;
    private int currentTypingIndex = 0;

    private void Start()
    {
        bool hasSeenIntro = PlayerPrefs.GetInt(introPlayerPrefsKey, 0) == 1;

        if (hasSeenIntro)
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(Typing(0));
    }

    private void Update()
    { 
        SkipTyping();
    }

    private void SkipTyping()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentTypingNumber < typingText[currentTypingIndex].Length)
            {
                currentTypingNumber = typingText[currentTypingIndex].Length - 1;
            }
            else if(currentTypingNumber >= typingText[currentTypingIndex].Length)
            {
                currentTypingIndex++;
                
                if (currentTypingIndex > typingText.Length - 1)
                {
                    PlayerPrefs.SetInt(introPlayerPrefsKey, 1);
                    StopAllCoroutines();
                    this.gameObject.SetActive(false);
                    return;
                }
                
                StartCoroutine(Typing(currentTypingIndex));
            }
        }
    }


    IEnumerator Typing(int index)
    {
        if(index ==  0)
        {
            yield return new WaitForSeconds(1.0f);
        }

        for(currentTypingNumber = 0; currentTypingNumber <= typingText[index].Length; currentTypingNumber++)
        {
            text.text = typingText[index].Substring(0, currentTypingNumber);

            yield return new WaitForSeconds(0.1f);
        }

        StopAllCoroutines();

        yield break;
    }
}
