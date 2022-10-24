using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FactoryType", fileName = "FactoryType")]
public class FactoryType : ScriptableObject
{
    #region Enemies
    public GameObject blueEnemy;
    public GameObject brownEnemy;
    #endregion

    public GameObject shuriken;

    public GameObject rocket;
}
