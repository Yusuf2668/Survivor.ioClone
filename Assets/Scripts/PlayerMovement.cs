using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerBodySprite;

    [SerializeField] private float speed;

    [SerializeField] private DynamicJoystick dynamicJoystick;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 direction = Vector3.forward * dynamicJoystick.Vertical + Vector3.right * dynamicJoystick.Horizontal;
        if (direction == Vector3.zero)
        {
            _animator.SetBool("Run", false);
        }
        else
        {
            _animator.SetBool("Run", true);
        }
        if (direction.x < 0)
        {
            playerBodySprite.flipX = true;
        }
        else
        {
            playerBodySprite.flipX = false;
        }
        transform.position += new Vector3(direction.x, direction.z) * Time.deltaTime * speed;
    }
}
