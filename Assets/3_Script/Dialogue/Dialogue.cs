using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public bool choice;
    public int[] choiceId;
    public int reward;
    public string name;
    [TextArea(3,10)]
    public string[] sentences;
    public string[] choiceSentences;
}
