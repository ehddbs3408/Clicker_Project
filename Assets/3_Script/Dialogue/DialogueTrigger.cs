using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void OnClickTriggerDialogue()
    {
        FindObjectOfType<DialogueData>().StartDialogue(0);
    }
}
