using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkillButton : MonoBehaviour
{
    [SerializeField] private GameObject meteorSkillPrefab;

    public void UseMeteorSkill()
    {
        Instantiate(meteorSkillPrefab, Vector3.zero, Quaternion.identity);
    }
}