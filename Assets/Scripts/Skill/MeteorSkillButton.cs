using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSkillButton : MonoBehaviour
{
    [SerializeField] private float skillCooldownTime;
    [SerializeField] private Image skillImage;
    [SerializeField] private GameObject meteorSkillPrefab;

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
        if (skillTime >= skillCooldownTime)
        {
            skillButton.interactable = true;
            
            return;
        }

        skillTime += Time.deltaTime;

        skillImage.fillAmount = skillTime / skillCooldownTime;
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