using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemController : MonoBehaviour, IGem
{
    public float expCount => _expCount;

    [SerializeField] private float _expCount;

    private void OnEnable()
    {
        EventManager.Instance.magnetCollect += TakeGem;
    }
    public void TakeGem()
    {
        transform.SetParent(PlayerController.Instance.transform);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + (transform.position - PlayerController.Instance.transform.position), .3f));
        sequence.Append(transform.DOLocalMove(Vector3.zero, .3f)).OnComplete(() => gameObject.SetActive(false));
    }
}
