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
    void Start()
    {
        UpdateLovePanel();
    }
    public void OnClickLove()
    {
        GameManager.Instance.CurrentUser.love += 1;
        UpdateLovePanel();
    }
    public void UpdateLovePanel()
    {
        loveText.text = string.Format("{0} 호감도",GameManager.Instance.CurrentUser.love);
    }
}
