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
    private Button puchaseButton = null;
    [SerializeField]
    private Sprite[] statSprite = null;

    private Stat stat = null;

    private const int clickConst1=10;
    private const int clickConst2=10;

    public void SetValue(Stat stat)
    {
        this.stat = stat;
        UpdateUI();
    }
    public void UpdateUI()
    {
        //statImage.sprite = statSprite[stat.imageNumber];
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
        statInList.price = (long)(Mathf.Pow(statInList.level,2)*(clickConst1*(statInList.level-1))+clickConst2);
        UpdateUI();
        GameManager.Instance.uIManager.UpdateLovePanel();

    }
}
