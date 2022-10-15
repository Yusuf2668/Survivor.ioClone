using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] float followSpeed;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    private void LateUpdate()
    {
        gameObject.transform.DOMove(new Vector3(Mathf.Clamp(_player.transform.position.x, -30f, 30f), Mathf.Clamp(_player.transform.position.y,-30f,30f) , gameObject.transform.position.z), followSpeed);
    }
}
