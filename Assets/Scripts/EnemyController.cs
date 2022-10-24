using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] EnemyType enemyType;

    private float _enemyHealth;

    private Transform _playerTransform;
    private Vector3 _direction;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;
    private HitTextController _hitTextController;
    private GameObject _gem;

    private void Awake()
    {
        _gem = transform.GetChild(0).gameObject;
        _hitTextController = gameObject.GetComponentInChildren<HitTextController>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        _enemyHealth = enemyType.enemyHealth;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        _direction = _playerTransform.position - transform.position;
        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        if (_enemyHealth > 0)
        {
            transform.position += _direction * Time.deltaTime * enemyType.enemySpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        if (_enemyHealth <= 0)
        {
            return;
        }
        _enemyHealth -= damage;
        _hitTextController.ShowText(damage);
        if (_enemyHealth <= 0)
        {
            _gem.SetActive(true);
            _gem.transform.SetParent(null);
            _animator.SetTrigger("Die");
            _spriteRenderer.sortingOrder = 1;
            _capsuleCollider2D.isTrigger = true;
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        _hitTextController.ClearText();
        _spriteRenderer.sortingOrder = 2;
        _capsuleCollider2D.isTrigger = false;
        this.enabled = true;
    }

    public void Die()//Animator içinde kullanmak için
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Attack", true);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("Attack", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shuriken"))
        {
            _animator.SetTrigger("Hurt");
        }
    }
}
