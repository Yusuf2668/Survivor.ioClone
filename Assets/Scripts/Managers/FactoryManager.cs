using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoSingleton<FactoryManager>
{
    [SerializeField] FactoryType factoryType;

    public GameObject CreateNewObject(string objectName, Transform parent)
    {
        GameObject resultObject = null;
        switch (objectName)
        {
            case "BlueEnemy":
                resultObject = Instantiate(factoryType.blueEnemy, parent);
                break;
            case "Shuriken":
                resultObject = Instantiate(factoryType.shuriken, parent);
                break;
            case "Rocket":
                resultObject = Instantiate(factoryType.rocket, parent);
                break;
        }
        return resultObject;
    }
}
