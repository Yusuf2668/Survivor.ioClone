using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoSingleton<LevelManager>, ILevelManager
{
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI levelText;

    string level_Key = "levelKey";
    string exp_Key = "expKey";

    private float _level;
    private float _exp;

    private void Awake()
    {
        _level = PlayerPrefs.GetFloat(level_Key, 1);
        _exp = PlayerPrefs.GetFloat(exp_Key, 0.25f);
        levelText.text = _level.ToString();
        expBar.value = _exp;
    }

    public void AddExp(float exp)
    {
        _exp += exp;
        PlayerPrefs.SetFloat(exp_Key, _exp);
        expBar.value = _exp;
        LevelUpdate();
    }

    public void LevelUpdate()
    {
        if (expBar.value >= 0.9f)
        {
            _exp = 0;
            _level++;
            PlayerPrefs.SetFloat(level_Key, _level);
            levelText.text = _level.ToString();
        }
    }
}
