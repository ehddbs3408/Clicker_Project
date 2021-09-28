using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Image statImage = null;
    [SerializeField]
    private Text statnameText = null;
    [SerializeField]
    private Text statlavelText = null;
    [SerializeField]
    private Text statpriceText = null;
    [SerializeField]
    private Sprite[] statSprite = null;
    private Stat stat = null;

    private const int clickConst1=1;
    private const int clickConst2=5;

    public void SetValue(Stat stat)
    {
        this.stat = stat;
        UpdateUI();
    }
    private void UpdateUI()
    {
        statImage.sprite = statSprite[stat.imageNumber];
        statnameText.text = stat.name;
        statlavelText.text = string.Format("Lv.{0}",stat.level);
        statpriceText.text = string.Format("{0}",stat.price);
    }
    public void OnclickPurchase()
    {
        if(GameManager.Instance.CurrentUser.love<stat.price) return;
        GameManager.Instance.CurrentUser.love -= stat.price;
        Stat statInList = GameManager.Instance.CurrentUser.statList.Find((x)=> x.name == stat.name);
        statInList.level++;
        if(statInList.eCl==1)
        {
            statInList.price = (long)(statInList.level*(clickConst1*(statInList.level-1))+clickConst2);
        }
        else
        {
            //statInList.price = (long)(
        }
        UpdateUI();
        GameManager.Instance.uIManager.UpdateLovePanel();
    }
}
