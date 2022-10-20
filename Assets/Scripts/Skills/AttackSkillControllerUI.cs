using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkillControllerUI : MonoBehaviour, IAtackSkill
{
    public void UpdateShootCount()
    {
        PlayerController.Instance.ShootCount++;
    }
}
