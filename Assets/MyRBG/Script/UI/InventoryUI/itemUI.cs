using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;

    public void InitItem(Sprite IconSprite , string name , string type)
    {
        iconImage.sprite = IconSprite;
        nameText.text = name;
        typeText.text = type;
    }
}
