using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour, IRocketController
{
    [SerializeField] private GameObject _animator;

    private void Start()
    {
        //belli bir s�re sonra kendisi otomatik olarak kapan�cak
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<EnemyController>().TakeDamage(200);
        if (_animator.activeInHierarchy) return;
        _animator.SetActive(true);
        Invoke("Destory", 0.34f);
    }
    public void Destory()//0.35 second after
    {
        gameObject.SetActive(false);
    }
}
