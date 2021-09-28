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
    private List<UpgradePanel> upgradePanelsList = new List<UpgradePanel>();
    private List<EventPanel> eventPanelsList = new List<EventPanel>();
    private int clickLoveAdd = 0;
    private bool isUpPanel = true;
    private bool isTyping = true;
    private int randomNumber;
    void Start()
    {
        
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
        loveText.text = string.Format("{0:###,###,###,###0} 호감도",GameManager.Instance.CurrentUser.love);
    }
    private int EarnClickLove()
    {
        int clickLove = 0;
        
        foreach(Stat stat in GameManager.Instance.CurrentUser.statList)
        {
            while(stat.level >= 10*clickLoveAdd)
            {
                clickLoveAdd++;
                Debug.Log(clickLoveAdd);
            }
            clickLove += stat.eCl * clickLoveAdd * stat.level;
        }
        return clickLove;
    }
    private void RandomDialogue()
    {
        randomNumber = Random.Range(1,10);
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
    public void OnClickQuit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    
}
