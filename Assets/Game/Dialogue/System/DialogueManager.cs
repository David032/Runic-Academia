using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    public GameObject DialogueWindow;
    public TextMeshProUGUI DialogueText;
    public Image DialogueIcon;

    public void SetDialogue(string Dialogue) 
    {
        DialogueText.text = Dialogue;
    }
    public void SetDialogue(string Dialogue, Sprite DialogueImage)
    {

    }

    public void ToggleWindow() 
    {
        DialogueWindow.SetActive(!DialogueWindow.activeSelf);
    }

    public void ShowWindow() 
    {
        
    }
}
