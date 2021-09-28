using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LovePointSilder : MonoBehaviour
{
    [SerializeField]
    private Slider lovePointSlider;
    void Update()
    {
        UpdateLovePoint();
    }
    private void UpdateLovePoint()
    {
        lovePointSlider.value = GameManager.Instance.CurrentUser.lovePoint;
    }
}
