using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableDoll : MonoBehaviour
{
    int _type;
    
    public void SetVegetableType(int type)
    {
        _type = type;
    }

    public int GetVegetableType()
    {
        return _type;
    }

}
 