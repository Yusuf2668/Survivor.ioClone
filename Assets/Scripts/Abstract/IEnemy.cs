using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy
{
    void MoveToPlayer();

    void TakeDamage(float damage);

    void Die();
}
