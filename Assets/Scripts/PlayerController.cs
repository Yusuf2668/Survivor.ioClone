using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoSingleton<PlayerController>, IPlayer
{
    public float ShootCount
    {
        get { return _shootCount; }
        set
        {
            if (_shootCount < 5)
            {
                _shootCount = value;
            }
        }
    }
    public float ItemColliderRadius
    {
        get { return _itemColliderRadius; }
        set
        {
            if (_itemColliderRadius < 5)
            {
                _itemColliderRadius = value;
                itemCollider.radius = _itemColliderRadius;
                PlayerPrefs.SetFloat(radiusPrefs, _itemColliderRadius);
            }
        }
    }

    public PlayerSkillType playerSkillType;

    [SerializeField] private CircleCollider2D itemCollider;

    [SerializeField] private Slider expSliderBar;
    [SerializeField] private Image healthBar;

    [SerializeField] private float shootForce;
    [SerializeField] private float shootrepeatTime;
    [SerializeField] private float health;
    [SerializeField] private float speed;

    [SerializeField] private DynamicJoystick _dynamicJoystick;
    [SerializeField] private SpriteRenderer _playerBodySprite;


    private string radiusPrefs = "radiusPrefs";
    [HideInInspector] public string rocketSkillHavePrefs = "RocketHave";

    private Vector3 _direction;

    private float _distanceToCloselyTarget;
    private float _distanceToTarget;
    private float _shootCount = 1;//ayný anda birden çok saldýrý yapmasýna yarýyor
    private float _shootrepeatTime;
    private float _itemColliderRadius;

    private Transform _target;

    private Animator _animator;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(rocketSkillHavePrefs, 0) == 1)
        {
            gameObject.AddComponent<RocketLunchSkill>();
        }
        _itemColliderRadius = PlayerPrefs.GetFloat(radiusPrefs, 0.5f);
        itemCollider.radius = _itemColliderRadius;
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
            FindCloselyEnemy();
            Shoot();
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
        StartCoroutine("ShootEnumator");
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -32.5f, 32.5f), Mathf.Clamp(transform.position.y, -34f, 34));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Gem":
                GemController gemController = collision.GetComponent<GemController>();
                LevelManager.Instance.AddExp(gemController.expCount * 2);
                gemController.TakeGem();
                break;
            case "Magnet":
                EventManager.Instance.MagnetCollect();
                collision.gameObject.SetActive(false);
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount -= damage / 100;
        if (health <= 0)
        {
            _animator.SetTrigger("Death");
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            this.enabled = false;
        }
    }

    IEnumerator ShootEnumator()
    {
        for (int i = 0; i < _shootCount; i++)
        {
            _direction = FindCloselyEnemy().transform.position - transform.position;
            ObjectPoolManager.Instance.GetPoolObject("Shuriken", transform.position).GetComponent<Rigidbody2D>().velocity = new Vector2(_direction.x, _direction.y).normalized * shootForce * Time.deltaTime;
            yield return new WaitForSeconds(.2f);
        }
    }
}
