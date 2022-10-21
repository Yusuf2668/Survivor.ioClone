using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour, IRocketController
{
    [SerializeField] private GameObject _animator;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("Destory", 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<EnemyController>()) return;
        rigidbody2D.velocity = Vector3.zero;
        collision.GetComponent<EnemyController>().TakeDamage(200);
        _animator.SetActive(true);
        Invoke("Destory", 0.34f);
    }
    public void Destory()//0.35 second after
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _animator.SetActive(false);
    }
}
