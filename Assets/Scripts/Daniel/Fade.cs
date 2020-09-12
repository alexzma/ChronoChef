using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer render;
    float fadeSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = render.color;
        temp.a -= Time.deltaTime * fadeSpeed;
        if (temp.a <= 0)
            Destroy(this.gameObject);
        else
            render.color = temp;
    }
}
