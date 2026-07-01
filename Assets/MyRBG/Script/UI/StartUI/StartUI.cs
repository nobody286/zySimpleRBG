using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour
{
    public Image startImage;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI startText2;
    public Button startButton;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void OnStartButtonClick()
    {
        Time.timeScale = 1;
        startImage.enabled = false;
        startText.enabled = false;
        startText2.enabled = false;
        startButton.gameObject.SetActive(false);
    }
}
