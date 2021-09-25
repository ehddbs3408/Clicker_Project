using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [SerializeField]
    private Image eventImage = null;
    [SerializeField]
    private Text eventNameText = null;
    [SerializeField]
    private Text eventPriceText = null;
    [SerializeField]
    private Sprite[] sprites = null; 
    private Event event_ = null;
    
    public void SetValue(Event event_)
    {
        this.event_ = event_;
        UpdateUI();
    }

    public void UpdateUI()
    {
        //eventImage.sprite = sprites[event_.imageNumber];
        eventNameText.text = event_.name;
        eventPriceText.text = string.Format("{0}",event_.price);
    }

    public void OnclickPurchase()
    {
        
    }

}
