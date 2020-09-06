using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodItemManager : MonoBehaviour
{
    public GameObject m_image;
    public GameObject m_x;
    public Text m_text;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    public void SetText(string text)
    {
        m_text.text = text;
    }

    public void Validate()
    {
        //change the image to check mark
        m_x.SetActive(false);
        m_image.SetActive(true);
    }
}
