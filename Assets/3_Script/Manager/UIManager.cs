using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text loveText = null;
    [SerializeField]
    private UpgradePanel upgradePanelTemple = null;
    [SerializeField]
    private EventPanel eventPanelTemple = null;
    [SerializeField]
    private GameObject updownPanel = null;
    [SerializeField]
    private GameObject lovePrefabTemple= null;
    [SerializeField]
    private GameObject randomDialoguePanel;
    [SerializeField]
    private Text randomDialogueText;
    [SerializeField]
    private RandomDialogue randomDialogue;
    [SerializeField]
    private GameObject startPanel,offPanel,introPanel;
    private List<UpgradePanel> upgradePanelsList = new List<UpgradePanel>();
    private List<EventPanel> eventPanelsList = new List<EventPanel>();
    private int loveAdd = 0;
    private bool isUpPanel = true;
    private bool isTyping = true;
    private int randomNumber;
    private int intro = 0;

    void Start()
    {
        intro = PlayerPrefs.GetInt("Intro",7);
        UpdateLovePanel();
        CreateUpgradePanel();
        CreateEventPanel();
    }
    private void CreateUpgradePanel()
    {
        GameObject panel = null;
        UpgradePanel panelComponent = null;
        foreach(Stat stat in GameManager.Instance.CurrentUser.statList)
        {
            panel = Instantiate(upgradePanelTemple.gameObject,upgradePanelTemple.transform.parent);
            panelComponent = panel.GetComponent<UpgradePanel>();
            panelComponent.SetValue(stat);
            panel.SetActive(true);
            upgradePanelsList.Add(panelComponent);
        }
    }
    private void CreateEventPanel()
    {
        GameObject panel = null;
        EventPanel panelComponent = null;
        foreach(Event evnet in GameManager.Instance.CurrentUser.evevntList)
        {
            panel = Instantiate(eventPanelTemple.gameObject,eventPanelTemple.transform.parent);
            panelComponent = panel.GetComponent<EventPanel>();
            panelComponent.SetValue(evnet);
            panel.SetActive(true);
            eventPanelsList.Add(panelComponent);
        }

    }
    public void OnClickLove()
    {
        int i=0;
        GameManager.Instance.CurrentUser.love += EarnClickLove();
        UpdateLovePanel();
        if(isTyping)
        RandomDialogue();
        while(i<=2)
        {
            GameObject lovePrefab = null;
            Vector2 dir =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(GameManager.Instance.poolManager.transform.childCount > 0)
            {
            
                lovePrefab = GameManager.Instance.poolManager.transform.GetChild(0).gameObject;
                lovePrefab.GetComponent<LoveClick>().Set();
                lovePrefab.transform.position = dir;
            }
            else
            {
                Debug.Log("wfa");
                lovePrefab = Instantiate(lovePrefabTemple,dir,Quaternion.identity);
            }
            if(lovePrefab != null)
            {
                lovePrefab.transform.SetParent(null);
            }
            lovePrefab.SetActive(true);
            i++;
        }
        

    }
    public void OncliclUpDownPanel()
    {
        float upDownY = 1020;
        if(isUpPanel)
        {
            isUpPanel = false;
            updownPanel.GetComponent<RectTransform>().DOAnchorPosY(updownPanel.GetComponent<RectTransform>().anchoredPosition.y-upDownY,0.5f);
            
        }
        else
        {
            isUpPanel = true;
            updownPanel.GetComponent<RectTransform>().DOAnchorPosY(updownPanel.GetComponent<RectTransform>().anchoredPosition.y+upDownY,0.5f);
        }
        
    }
    public void UpdateLovePanel()
    {
        loveText.text = string.Format("{0:###,###,###,###,###,###0}",GameManager.Instance.CurrentUser.love); 
    }
    private int EarnClickLove()
    {
        int clickLove = 0;
        
        foreach(Stat stat in GameManager.Instance.CurrentUser.statList)
        {
            
            while(stat.level >= 10*loveAdd)
            {
                loveAdd++;
                Debug.Log(loveAdd);
            }
            clickLove += stat.eCl * loveAdd * stat.level;
        }
        return clickLove;
    }
    private void RandomDialogue()
    {
        int lovePointAdd = 0;
        while(GameManager.Instance.CurrentUser.lovePoint > lovePointAdd)
        {
            lovePointAdd++;
        }
        randomNumber = Random.Range(1,10);
        randomNumber += lovePointAdd * 10;
        Debug.Log(randomNumber);
        
        StartCoroutine(RandomDialogueWindow());
        
        StartCoroutine(TypeSentence(randomDialogue.randomDialogue[randomNumber]));
    }
    private IEnumerator RandomDialogueWindow()
    {
        isTyping = false;
        randomDialoguePanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        randomDialoguePanel.SetActive(false);
        yield return new WaitForSeconds(5f);
        isTyping = true;
    }
    private IEnumerator TypeSentence(string sentence)
    {
        randomDialogueText.text = "";
        foreach(char latter in sentence.ToCharArray())
        {
            randomDialogueText.text += latter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void OnClickStart()
    {
        if(intro == 1)
        {
            startPanel.SetActive(true);
            offPanel.SetActive(false);
            Debug.Log("Game Start");
            return;
        }
        intro = 1;
        PlayerPrefs.SetInt("Intro",intro);
        DialogueData.Instance.StartDialogue(0);
        offPanel.SetActive(false);
        introPanel.SetActive(true);
        Debug.Log("Intro Start");
    }
    public void OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    
}
