using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Outro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private string[] typingText = { "To Be Continue"
    };

    public string ontroPlayerPrefsKey = "HasSeenOutro";

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
            if (currentTypingNumber < typingText[currentTypingIndex].Length)
            {
                currentTypingNumber = typingText[currentTypingIndex].Length - 1;
            }
            else if (currentTypingNumber >= typingText[currentTypingIndex].Length)
            {
                currentTypingIndex++;

                if (currentTypingIndex > typingText.Length - 1)
                {
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
        if (index == 0)
        {
            yield return new WaitForSeconds(1.0f);
        }

        for (currentTypingNumber = 0; currentTypingNumber <= typingText[index].Length; currentTypingNumber++)
        {
            text.text = typingText[index].Substring(0, currentTypingNumber);

            yield return new WaitForSeconds(0.1f);
        }

        StopAllCoroutines();

        yield break;
    }
}
