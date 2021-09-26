using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
   [SerializeField]
   private GameObject[] OffPanel = null;
   [SerializeField]
   private GameObject onPanel = null;
   public void OnclickPurchase()
   {
       for(int i = 0;i<OffPanel.Length;i++)
       {
           OffPanel[i].SetActive(false);
       }
       onPanel.SetActive(true);
   } 
}
