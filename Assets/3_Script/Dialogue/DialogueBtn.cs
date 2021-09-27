using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBtn : MonoBehaviour
{
    [SerializeField]
    private int choiceNumber;
   
    public void OnClickTriggerDialogue()
    {
        FindObjectOfType<DialogueData>().StartDialogue
        (DialogueData.Instance.CurrentDialogueGroup.dialogueList[DialogueData.Instance.CurrentDialogueGroup.id].choiceId[choiceNumber]);
    }
}
