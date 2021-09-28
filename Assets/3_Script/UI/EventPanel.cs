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
    private Text repalyText = null;
    [SerializeField]
    private Sprite[] sprites = null; 
    [SerializeField]
    private GameObject onEventUi , offInGameUi;
    private Event event_ = null;
    
    public void SetValue(Event event_)
    {
        this.event_ = event_;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //eventImage.sprite = sprites[event_.imageNumber];
        eventNameText.text = event_.name;
        eventPriceText.text = string.Format("{0}",event_.price);
        if(event_.replay)
        repalyText.text = string.Format("다시 보기");
    }

    public void OnclickPurchase()
    {
        if(event_.replay)
        {
            TriggerDialogue(event_.eventId);
            onEventUi.SetActive(true);
            offInGameUi.SetActive(false);
            return;
        }
        if(GameManager.Instance.CurrentUser.love < event_.price) return;
        GameManager.Instance.CurrentUser.love -= event_.price;
        event_.replay = true;
        
        repalyText.text = string.Format("다시 보기");
        event_.price = 0;
        onEventUi.SetActive(true);
        offInGameUi.SetActive(false);
        TriggerDialogue(event_.eventId);
        UpdateUI();
    }
    private void TriggerDialogue(int id)
    {
        FindObjectOfType<DialogueData>().StartDialogue(id);
    }

}
