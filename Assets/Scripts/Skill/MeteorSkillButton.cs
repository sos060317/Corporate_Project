using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeteorSkillButton : MonoBehaviour
{
    [SerializeField] private float skillCooldownTime;
    [SerializeField] private Image skillImage;
    [SerializeField] private GameObject meteorSkillPrefab;
    [SerializeField] private TextMeshProUGUI coolTimeText;

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
            coolTimeText.gameObject.SetActive(false);

            return;
        }

        if (!GameManager.Instance.isUseSkill)
        {
            skillTime += Time.deltaTime;

            coolTimeText.gameObject.SetActive(true);
            coolTimeText.text = Mathf.CeilToInt(skillCooldownTime - skillTime).ToString();
        }

        skillImage.fillAmount = skillTime / (skillCooldownTime * GameManager.Instance.skillCoolTimeMultiply);
    }

    public void UseMeteorSkill()
    {
        if (GameManager.Instance.isUseSkill)
        {
            return;
        }
        
        skillButton.interactable = false;

        skillTime = 0;

        Instantiate(meteorSkillPrefab, Vector3.zero, Quaternion.identity);
        
        GameManager.Instance.isUseSkill = true;
    }
}