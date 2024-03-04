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
    };

    private int currentTypingNumber = 0;
    private int currentTypingIndex = 0;

    private void Start()
    {
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
            if (currentTypingIndex > typingText.Length - 1)
            {
                StopAllCoroutines();
                this.gameObject.SetActive(false);
                return;
            }

            // 이거 지금 이상하니깐 고쳐야함 
            // 서로 검사하는 인덱스가 다름
            // currentTypingNumber 초기화가 않된 상태에서 비교함
            if (currentTypingNumber < typingText[currentTypingIndex].Length)
            {
                currentTypingNumber = typingText[currentTypingIndex].Length - 1;
            }
            else if(currentTypingNumber >= typingText[currentTypingIndex].Length)
            {
                StartCoroutine(Typing(currentTypingIndex));
            }
        }
    }


    IEnumerator Typing(int index)
    {
        if(index ==  0)
        {
            yield return new WaitForSeconds(2.0f);
        }

        for(; currentTypingNumber <= typingText[index].Length; currentTypingNumber++)
        {
            text.text = typingText[index].Substring(0, currentTypingNumber);

            yield return new WaitForSeconds(0.1f);
        }

        currentTypingIndex++;
        StopAllCoroutines();

        yield break;
    }
}
