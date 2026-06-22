using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{   
    public static DialogUI Instance { get; private set; }
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public Button continueButton;
    private List<string> contentList;
    private int contentIndex = 0;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        Hide();
        continueButton.onClick.AddListener(this.OnContinueButtonOnClick);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Show(string name, string[] content)
    {

        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentIndex = 0;
        contentText.text = contentList[0];
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnContinueButtonOnClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            Hide();
            return;
        }
  
        contentText.text = contentList[contentIndex];
        
    }
   
}
