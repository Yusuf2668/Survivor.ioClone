using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLunchSkill : MonoBehaviour
{
    [SerializeField]
    PlayerSkillType skillType;

    private float _rocketLaunchCount;

    private Vector3 _direction;

    private EnemyController[] enemy;

    private void Awake()
    {
        _rocketLaunchCount = skillType.rocketRepeatTime;
    }

    private void Update()
    {
        _rocketLaunchCount -= Time.deltaTime;
        if (_rocketLaunchCount <= 0)
        {
            _rocketLaunchCount = skillType.rocketRepeatTime;
            LaunchRocket();
        }
    }

    private void LaunchRocket()
    {
        enemy = GameObject.FindObjectsOfType<EnemyController>();
        _direction = enemy[Random.Range(0, enemy.Length)].transform.position - transform.position;
        ObjectPoolManager.Instance.GetPoolObject("Rocket", transform.position).GetComponent<Rigidbody2D>().velocity = new Vector2(_direction.x, _direction.y).normalized * skillType.rocketSpeed * Time.deltaTime;
    }
}
