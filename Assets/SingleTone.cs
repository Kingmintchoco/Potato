using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTone<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();

            if(instance == null)
            {
                instance = new GameObject(typeof(T).ToString(), typeof(T)).AddComponent<T>();
            }

            return instance;
        }
    }
}
