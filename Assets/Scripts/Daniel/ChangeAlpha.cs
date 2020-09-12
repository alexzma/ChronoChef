using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeAlpha : MonoBehaviour
{
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = sprite;
        Color temp = image.color;
        temp.a = 0.75f;
        image.color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
