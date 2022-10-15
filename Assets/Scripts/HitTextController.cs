using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class HitTextController : MonoBehaviour
{
    private TextMeshProUGUI hitText;

    private void Awake()
    {
        hitText = GetComponent<TextMeshProUGUI>();
    }

    public void ShowText(float damage)
    {
        hitText.text = damage.ToString();
        hitText.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
        hitText.transform.DOLocalMoveY(-0.3f, 1f);
    }
}
