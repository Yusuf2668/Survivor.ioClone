using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RocketLunchSkill : MonoBehaviour
{
    private float _rocketLaunchCount;

    private Vector3 _direction;

    private EnemyController[] enemy;

    private void Awake()
    {
        _rocketLaunchCount = PlayerController.Instance.playerSkillType.rocketRepeatTime;
    }

    private void Update()
    {
        _rocketLaunchCount -= Time.deltaTime;
        if (_rocketLaunchCount <= 0)
        {
            _rocketLaunchCount = PlayerController.Instance.playerSkillType.rocketRepeatTime;
            LaunchRocket();
        }
    }

    private void LaunchRocket()
    {
        enemy = GameObject.FindObjectsOfType<EnemyController>();
        Vector3 randomEnemyPosition = enemy[Random.Range(0, enemy.Length)].transform.position;
        GameObject rocket = ObjectPoolManager.Instance.GetPoolObject("Rocket", transform.position);
        _direction = (randomEnemyPosition - rocket.transform.position).normalized;
        float rot = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        rocket.transform.rotation = Quaternion.Euler(0, 0, rot);
        rocket.GetComponent<Rigidbody2D>().AddForce(_direction * PlayerController.Instance.playerSkillType.rocketSpeed * Time.deltaTime);
    }
}
