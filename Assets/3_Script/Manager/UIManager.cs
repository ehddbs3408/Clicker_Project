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
    private GameObject UpdownPanel = null;
    [SerializeField]
    private GameObject lovePrefabTemple= null;
    private List<UpgradePanel> upgradePanelsList = new List<UpgradePanel>();
    private List<EventPanel> eventPanelsList = new List<EventPanel>();
    private int clickLoveAdd = 0;
    private bool isUpPanel = true;
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
        
        if(isUpPanel)
        {
            isUpPanel = false;
            UpdownPanel.GetComponent<RectTransform>().DOAnchorPosY(UpdownPanel.GetComponent<RectTransform>().anchoredPosition.y-1200f,0.5f);
            
        }
        else
        {
            isUpPanel = true;
            UpdownPanel.GetComponent<RectTransform>().DOAnchorPosY(UpdownPanel.GetComponent<RectTransform>().anchoredPosition.y+1200f,0.5f);
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
    private IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0.4f;
        Vector3 wasPos = a.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
    
}
