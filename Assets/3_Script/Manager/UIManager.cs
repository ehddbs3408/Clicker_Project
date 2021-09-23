using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text loveText = null;
    [SerializeField]
    private UpgradePanel upgradePanelTemple = null;
    private List<UpgradePanel> upgradePanelsList = new List<UpgradePanel>();
    private int clickLoveAdd = 0;
    void Start()
    {
        UpdateLovePanel();
        CreatePanel();
    }
    public void CreatePanel()
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
    public void OnClickLove()
    {
        GameManager.Instance.CurrentUser.love += EarnClickLove();
        UpdateLovePanel();
    }
    public void UpdateLovePanel()
    {
        loveText.text = string.Format("{0} 호감도",GameManager.Instance.CurrentUser.love);
    }
    private int EarnClickLove()
    {
        int clickLove = 0;
        
        foreach(Stat stat in GameManager.Instance.CurrentUser.statList)
        {
            if(stat.level >= 10*clickLoveAdd)
            {
                clickLoveAdd++;
            }
            clickLove += stat.eCl * clickLoveAdd * stat.level;
        }
        return clickLove;
    }
}
