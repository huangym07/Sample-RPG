using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCObject : InteractbleObject
{
    public string NPCName;
    public string[] contentList;
    protected override void Interact()
    {
        DialogueUI.Instance.Show(NPCName, contentList);
        // DialogueUI test = new DialogueUI(); // 尽管 DialogueUI 的默认构造函数并不是 private 的，但是 DialogueUI 继承于 MonoBehaviour，MonoBehaviour 是不允许用 new 来实例化的
    }
}
