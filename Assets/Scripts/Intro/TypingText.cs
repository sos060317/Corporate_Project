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
                                    "하지만 유일하게 원숭이들만 능력을 찾지 못하고 있었는데...",

                                    "다른 종족들은 그런 원숭이들을"




    };

    private int currentTypingNumber = 0;

    private void Start()
    {
        StartCoroutine(Typing(typingText[0]));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentTypingNumber = typingText.Length - 1;
        }
    }


    IEnumerator Typing(string _text)
    {
        yield return new WaitForSeconds(2.0f);

        for(currentTypingNumber = 0; currentTypingNumber <= _text.Length; currentTypingNumber++)
        {
            text.text = _text.Substring(0, currentTypingNumber);

            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
