using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaSkillButton : MonoBehaviour
{
    [SerializeField] private float skillCooldownTime;
    [SerializeField] private Image skillImage;
    [SerializeField] private GameObject bananaSkillPrefab;

    private float skillTime;

    private Button skillButton;

    private void Awake()
    {
        skillButton = GetComponent<Button>();

        skillTime = skillCooldownTime;
    }

    private void Update()
    {
        SKillCooldownUpdate();
    }

    private void SKillCooldownUpdate()
    {
        if (skillTime / (skillCooldownTime * GameManager.Instance.skillCoolTimeMultiply) >= 1)
        {
            skillButton.interactable = true;
            
            return;
        }

        if (!GameManager.Instance.isUseSkill)
        {
            skillTime += Time.deltaTime;
        }

        skillImage.fillAmount = skillTime / (skillCooldownTime * GameManager.Instance.skillCoolTimeMultiply);
    }

    public void UseBananaSkill()
    {
        if (GameManager.Instance.isUseSkill)
        {
            return;
        }
        
        skillButton.interactable = false;

        skillTime = 0;
        
        Instantiate(bananaSkillPrefab, Vector3.zero, Quaternion.identity);

        GameManager.Instance.isUseSkill = true;
    }
}