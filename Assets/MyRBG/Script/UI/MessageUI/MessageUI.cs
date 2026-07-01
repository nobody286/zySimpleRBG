using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
    }
    private TextMeshProUGUI messageText;
    private void Update()
    {
        if(messageText.enabled)
        {
            Color color = messageText.color;
            float alpha = Mathf.Lerp(color.a , 0 , Time.deltaTime);
            messageText.color = new Color(color.r , color.g , color.b , alpha);
            if(alpha == 0)
            {
                messageText.enabled = false;
            }
        }
    }
    private void Start()
    {
        messageText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Hide();
    }
    public void Show(string message)
    {
        messageText.enabled = true;
        messageText.text = message;
        messageText.color = Color.white;
    }
    public void Hide()
    {
        messageText.enabled = false;
    }
}
