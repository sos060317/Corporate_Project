using UnityEngine;
using UnityEngine.UI;

public class MusicSilder : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private bool isMute;

    private float beforeVolume;

    private void Start()
    {
        if (SoundManager.Instance.bgmVolume == 0)
        {
            isMute = true;
        }
        else
        {
            isMute = false;
        }
        
        volumeSlider.value = SoundManager.Instance.bgmVolume;
        volumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.SetMusicVolume(val));
    }

    private void Update()
    {
        if (SoundManager.Instance.bgmVolume == 0)
        {
            isMute = true;
        }
        else
        {
            isMute = false;
        }
    }
}