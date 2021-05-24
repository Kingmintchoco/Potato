using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableDoll : MonoBehaviour
{
    int _type;

    private void Update()
    {
        Test();
    }
    void Test()
    {
        bool isColl = false;
        Collider2D[] attachColliders = new Collider2D[4]; 
        GetComponent<Rigidbody2D>().GetAttachedColliders(attachColliders);

        Debug.Log(attachColliders.Length);
        foreach(Collider2D coll in attachColliders)
        {
            Debug.Log(coll);
            if(coll == null || coll.gameObject == this.gameObject)
            {
                continue;
            }

            if(coll.transform.localPosition.y <= transform.localPosition.y)
            {
                isColl = true;
            }
        }

        if(!isColl)
        {
            transform.localPosition -= new Vector3(0.0f, 1.0f, 0.0f);
        }

    }


    public void SetVegetableType(int type)
    {
        _type = type;
    }

    public int GetVegetableType()
    {
        return _type;
    }

}
 