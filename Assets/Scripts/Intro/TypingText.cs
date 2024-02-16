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
        StartCoroutine(Typing(typingText[0]));
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

            if (currentTypingNumber < typingText[currentTypingIndex].Length)
            {
                currentTypingNumber = typingText[currentTypingIndex].Length - 1;
            }
            else
            {
                StartCoroutine(Typing(typingText[currentTypingIndex]));
            }
        }
    }


    IEnumerator Typing(string _text)
    {
        if(currentTypingIndex ==  0)
        {
            yield return new WaitForSeconds(2.0f);
        }

        for(currentTypingNumber = 0; currentTypingNumber <= _text.Length; currentTypingNumber++)
        {
            text.text = _text.Substring(0, currentTypingNumber);

            yield return new WaitForSeconds(0.1f);
        }

        currentTypingIndex++;

        yield break;
    }
}
