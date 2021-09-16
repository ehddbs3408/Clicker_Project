using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user = null;
    public User CurrentUser {get{return user;}}
    public UIManager uIManager {get;private set;}
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    private void Awake() 
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if(!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        uIManager = GetComponent<UIManager>();

        InvokeRepeating("SaveToJson",1f,60f);
    }
    private void SaveToJson()
    {   
        SAVE_PATH = Application.dataPath + "/Save";
        if(user == null) return;
        string json = JsonUtility.ToJson(user,true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME,json,System.Text.Encoding.UTF8);
    }
    private void LoadFromJson()
    {
        string json = "";
        if(File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }
    private void OnApplicationQuit() {
        SaveToJson();
    }

}
