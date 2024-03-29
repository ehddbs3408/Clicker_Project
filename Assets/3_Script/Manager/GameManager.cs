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
    public PoolManager poolManager {get;private set;}

    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private GameObject onGameObject,offGameObject;
    [SerializeField]
    private GameObject quitPanel;
    private bool intro = false;
    private int ePsLoveAdd = 0;
    public void EarnLovePerSecond()
    {
        
        foreach(Stat stat in user.statList)
        {
            
            while(stat.level >= 10*ePsLoveAdd)
            {
                ePsLoveAdd++;
                Debug.Log(ePsLoveAdd);
            }
            user.love +=  stat.ePs * stat.level * ePsLoveAdd;
        }
        uIManager.UpdateLovePanel();
    }
    private void Awake() 
    {
        //응너바보응나바보둘다바보
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if(!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        uIManager = GetComponent<UIManager>();
        poolManager = FindObjectOfType<PoolManager>();
        
        InvokeRepeating("SaveToJson",1f,60f);
        InvokeRepeating("EarnLovePerSecond",0f,1f);
    }
    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }
    private void SaveToJson()
    {   
        SAVE_PATH = Application.persistentDataPath  + "/Save";
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
    public void OnOffGameObJect()
    {
        onGameObject.SetActive(true);
        offGameObject.SetActive(false);
    }
}
