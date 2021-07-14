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
    public GameObject CharacterFrame;
    public Button DialogueButton;

    DialogueObject NextUp;

    void Start()
    {
        CharacterFrame.SetActive(false);
        DialogueWindow.SetActive(false);
    }

    public void ConfigureDialogue(DialogueObject @object) 
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>()
            .canAct = false;
        DialogueText.text = @object.Contents;
        if (@object.Graphic != null)
        {
            DialogueIcon.sprite = @object.Graphic;
            CharacterFrame.SetActive(true);
        }
        else
        {
            CharacterFrame.SetActive(false);
        }

        if (@object.NextMessage != null)
        {
            NextUp = @object.NextMessage;
            DialogueButton.onClick.AddListener(ShowNextObject);
        }
        else
        {
            DialogueButton.onClick.AddListener(HideWindow);
        }
    }

    public void ToggleWindow() 
    {
        DialogueWindow.SetActive(!DialogueWindow.activeSelf);
    }

    public void ShowWindow() 
    {
        DialogueWindow.SetActive(true);
    }

    public void HideWindow() 
    {
        DialogueWindow.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>()
            .canAct = true;
    }

    void ShowNextObject() 
    {
        ConfigureDialogue(NextUp);
    }
}
