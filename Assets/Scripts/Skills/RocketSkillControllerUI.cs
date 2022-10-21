using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketSkillControllerUI : MonoBehaviour, IRocketSkillControllerUI
{
    private RocketLunchSkill rocketLunchSkill;
    public void AddRocketLaunchingSkill()
    {
        rocketLunchSkill = PlayerController.Instance.gameObject.GetComponent<RocketLunchSkill>();
        EventManager.Instance.SkillSelected();
        if (rocketLunchSkill)
        {
            rocketLunchSkill.RocketCount++;
            return;
        }
        PlayerController.Instance.gameObject.AddComponent<RocketLunchSkill>();
        PlayerPrefs.SetInt(PlayerController.Instance.rocketSkillHavePrefs, 1);
    }
}
