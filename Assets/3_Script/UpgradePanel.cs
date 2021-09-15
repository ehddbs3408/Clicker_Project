using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Image statImage;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text lavelText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Button puchaseButton = null;
    [SerializeField]
    private Sprite[] statSprite = null;
    private Stat stat = null; 
    
}
