using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RocketLunchSkill : MonoBehaviour
{
    public float RocketCount
    {
        get { return _rocketCount; }
        set
        {
            if (_rocketCount < 5)
            {
                _rocketCount = value;
                PlayerPrefs.SetFloat(rocketCountKey, _rocketCount);
            }
        }
    }

    private string rocketCountKey = "rocketCountKey";

    [SerializeField] private float _rocketCount;
    private float _rocketLaunchCount;

    private Vector3 _direction;

    private EnemyController[] enemy;

    private void Awake()
    {
        _rocketCount = PlayerPrefs.GetFloat(rocketCountKey, 1);
        _rocketLaunchCount = PlayerController.Instance.playerSkillType.rocketRepeatTime;
    }

    private void Update()
    {
        _rocketLaunchCount -= Time.deltaTime;
        if (_rocketLaunchCount <= 0)
        {
            _rocketLaunchCount = PlayerController.Instance.playerSkillType.rocketRepeatTime;
            StartCoroutine("LaunchRocket");
        }
    }

    IEnumerator LaunchRocket()//býçaktaki gibi Ienumaroterle yap
    {
        for (int i = 0; i < _rocketCount; i++)
        {
            enemy = GameObject.FindObjectsOfType<EnemyController>();
            Vector3 randomEnemyPosition = enemy[Random.Range(0, enemy.Length)].transform.position;
            GameObject rocket = ObjectPoolManager.Instance.GetPoolObject("Rocket", transform.position);
            _direction = (randomEnemyPosition - rocket.transform.position).normalized;
            float rot = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            rocket.transform.rotation = Quaternion.Euler(0, 0, rot);
            rocket.GetComponent<Rigidbody2D>().AddForce(_direction * PlayerController.Instance.playerSkillType.rocketSpeed * Time.deltaTime);
            yield return new WaitForSeconds(.2f);
        }
    }
}
