using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDisplay : MonoBehaviour
{
    CharacterManager CharacterManager;
    public TextMeshProUGUI CharacterName;

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager = CharacterManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.SelectedEntity != null)
        {
            CharacterName.text = CharacterManager.SelectedEntity.
                GetComponent<Entity>().Name;
        }
        if (CharacterManager.SelectedEntity == null)
        {
            CharacterName.text = "";
        }

    }
}
