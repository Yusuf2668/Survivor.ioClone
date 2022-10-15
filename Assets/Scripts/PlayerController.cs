using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField] private GameObject knifeShootPoint;

    [SerializeField] private float shootForce;
    [SerializeField] private float shootrepeatTime;
    [SerializeField] private float health;
    [SerializeField] private float speed;

    [SerializeField] private DynamicJoystick _dynamicJoystick;
    [SerializeField] private SpriteRenderer _playerBodySprite;

    private Vector3 _direction;

    private float _distanceToCloselyTarget;
    private float _distanceToTarget;
    private float _shootCount = 1;//ayný anda birden çok saldýrý yapmasýna yarýyor
    private float _shootrepeatTime;

    private Transform _target;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _shootrepeatTime = shootrepeatTime;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _animator.SetBool("Run", true);
            PlayerMove();
        }
        else
        {
            _animator.SetBool("Run", false);
        }
        _shootrepeatTime -= Time.deltaTime;
        if (_shootrepeatTime <= 0)
        {
            _shootrepeatTime = shootrepeatTime;
            for (int i = 0; i < _shootCount; i++)
            {
                FindCloselyEnemy();
                Shoot();
            }
        }
    }

    public Transform FindCloselyEnemy()
    {
        _distanceToCloselyTarget = Mathf.Infinity;
        var allTargets = GameObject.FindObjectsOfType(typeof(EnemyController));
        for (int i = 0; i < allTargets.Length; i++)
        {
            _distanceToTarget = (allTargets[i].GetComponent<Transform>().position - gameObject.transform.position).sqrMagnitude;
            if (_distanceToTarget < _distanceToCloselyTarget)
            {
                _distanceToCloselyTarget = _distanceToTarget;
                _target = allTargets[i].GetComponent<Transform>();
            }
        }
        return _target;
    }

    public void Shoot()
    {
        if (_target == null) return;
        _direction = FindCloselyEnemy().transform.position - transform.position;
        ObjectPoolManager.Instance.GetPoolObject("Shuriken", transform.position).GetComponent<Rigidbody2D>().velocity = new Vector2(_direction.x, _direction.y).normalized * shootForce * Time.deltaTime;
    }
    private void PlayerMove()
    {
        Vector3 direction = Vector3.forward * _dynamicJoystick.Vertical + Vector3.right * _dynamicJoystick.Horizontal;
        if (direction.x < 0)
        {
            _playerBodySprite.flipX = true;
        }
        else
        {
            _playerBodySprite.flipX = false;
        }
        transform.position += new Vector3(direction.x, direction.z) * Time.deltaTime * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            health -= 10;
            if (health <= 0)
            {
                _animator.SetTrigger("Death");
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                this.enabled = false;
            }
        }
    }
}
