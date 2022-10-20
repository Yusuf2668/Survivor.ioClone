using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketSkillControllerUI : MonoBehaviour, IRocketSkillControllerUI
{
    public void AddRocketLaunchingSkill()
    {
        if (PlayerController.Instance.gameObject.GetComponent<RocketLunchSkill>()) return;
        PlayerController.Instance.gameObject.AddComponent<RocketLunchSkill>();
    }
}
