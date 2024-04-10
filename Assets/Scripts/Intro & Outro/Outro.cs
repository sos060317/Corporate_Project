using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Outro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private string typingText = "To Be Continue";

    private string outroPlayerPrefsKey = "HasSeenOutro";

    bool hasSeenIntro;

    private void Awake()
    {
        hasSeenIntro = PlayerPrefs.GetInt(outroPlayerPrefsKey, 1) == 1;
    }

    private void Start()
    {
        if (hasSeenIntro)
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i <= typingText.Length; i++)
        {
            text.text = typingText.Substring(0, i);

            yield return new WaitForSeconds(0.1f);
        }

        StopAllCoroutines();
        this.gameObject.SetActive(false);

        yield break;
    }
}
