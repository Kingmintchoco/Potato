using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeFace : MonoBehaviour
{
    public Image img;
    public Sprite touchSpr;

    public void ChangeImage()
    {
        img.sprite = touchSpr;
        Debug.Log("touch");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
