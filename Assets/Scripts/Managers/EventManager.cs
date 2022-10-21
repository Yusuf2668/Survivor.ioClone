using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    public event Action magnetCollect;
    public event Action skillSelected;

    public void MagnetCollect() { magnetCollect?.Invoke(); }
    public void SkillSelected() { skillSelected?.Invoke(); }
}
