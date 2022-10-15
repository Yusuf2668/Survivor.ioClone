using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayer
{
    Transform FindCloselyEnemy();
    void Shoot();
}
