using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemManager : MonoBehaviour
{
    public Image m_image;
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

    public void SetImage()
    {
        //TODO: implement
    }
}
