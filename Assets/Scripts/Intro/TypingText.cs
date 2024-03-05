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

                                    "�׸��Ͽ� ������ ������ �ڽŵ��� \"Ÿ��\"�� ���׷��̵� �� �� �ְ� �Ǿ���",

                                    "�׸��Ͽ�...\r\n" +
                                    "�� �ɷ����� ������ ������ �ٸ� �����鿡��\r\n" +
                                    "���ѱ� �ڽ��� ���並 ��ã�� ���ؼ� �ο��� �ɰ� �Ǵµ�...."
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
