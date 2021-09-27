using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueData : MonoBehaviour
{
    // 그.. 동윤아.. 그게 맞아...??
    //그게 맞냐고.....
    //ㄱ,ㄱ[그게맞냐고!!!!!!!!!!]
    [SerializeField]
    private DialogueGroup dialogueGroup = null;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(int id)
    {
        dialogueGroup.id = id;
        
        Debug.Log("Starting conversation with " + dialogueGroup.dialogueList[dialogueGroup.id].name);

        nameText.text = dialogueGroup.dialogueList[dialogueGroup.id].name;

        sentences.Clear();
        foreach(string sentence in dialogueGroup.dialogueList[dialogueGroup.id].sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char latter in sentence.ToCharArray())
        {
            dialogueText.text += latter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
