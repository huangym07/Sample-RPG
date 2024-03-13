using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    private TextMeshProUGUI nameText;
    private TextMeshProUGUI contentText;
    private Button continueButton;

    public List<string> contentList;
    private int contentTextIndex = 0;
    private GameObject ui;

    private Action OnDialogueFinish;
    void Awake()
    {
        if (Instance != null && Instance != this) // 如果 Instance 已经指向一个 DialogUI 实例，并且不是挂载在当前游戏物体上的脚本实例（判断这个的原因可能是同一个游戏物体可以调用多次 Awake()，所以 Instance 可能指向的就是自己这个游戏物体上挂载的脚本实例），那么就把当前的游戏物体销毁
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this; // Awake() 是 Unity 加载脚本实例时调用的方法，也就是调用 Awake() 时，this 已经指向一个类的实例了
    }
    void Start()
    {
        nameText = transform.Find("UI/NameTextBg/NameText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("UI/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("UI/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(this.OnContinueButtonClick);
        ui = transform.Find("UI").gameObject;
        Hide();
    }
    public void Show()
    {
        ui.SetActive(true);
    }
    public void Show(string name, string[] content, Action OnDialogFinish = null)
    {
        this.OnDialogueFinish = OnDialogFinish;
        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentTextIndex = 0;
        contentText.text = contentList[contentTextIndex];
        ui.SetActive(true);
        gameObject.SetActive(true);

    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    private void OnContinueButtonClick()
    {
        contentTextIndex++;
        if (contentTextIndex >= contentList.Count)
        {
            OnDialogueFinish?.Invoke();
            Hide(); return;
        }
        contentText.text = contentList[contentTextIndex];
    }
}
