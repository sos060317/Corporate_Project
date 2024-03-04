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
                                    "������ �����ϰ� ������ ������ �ɷ��� ã�� ���ϰ� �־��µ�...",

                                    "�̰��� ���������� ������ �ձ�\r\n" +
                                    "�ٸ� �������� �׷� ������ ������\r\n" +
                                    "������ ������ �ʾҴ�...",

                                    "�׷��� �����...",

                                    "������ ������ �ڽŵ��� �ձ� ����� ����\r\n" +
                                    "���������� �ɷ�, \"��ȭ\"�� ã�� �Ǿ���",
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

            // �̰� ���� �̻��ϴϱ� ���ľ��� 
            // ���� �˻��ϴ� �ε����� �ٸ�
            // currentTypingNumber �ʱ�ȭ�� �ʵ� ���¿��� ����
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
