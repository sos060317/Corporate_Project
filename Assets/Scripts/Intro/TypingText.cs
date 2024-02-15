using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class TypingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private string[] typingText = { "�̰��� ������ �ձ�\r\n" +
                                    "��� �������� �ڽŵ��� �ɷ��� ������ �ִ�\r\n" +
                                    "������ �����ϰ� �����̵鸸 �ɷ��� ã�� ���ϰ� �־��µ�...",

                                    "�ٸ� �������� �׷� �����̵���"




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
