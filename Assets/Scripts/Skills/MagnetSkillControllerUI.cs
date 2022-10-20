using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkillControllerUI : MonoBehaviour, IMagnet
{
    public void UpdateItemColliderRadius()
    {
        PlayerController.Instance.ItemColliderRadius += 0.5f;
    }
}
