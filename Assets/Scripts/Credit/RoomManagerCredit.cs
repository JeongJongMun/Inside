using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagerCredit : MonoBehaviour
{
    [Header("ũ����")]
    public CreditMove credit;

    [Header("�г�")]
    public Image panel;

    int panelAlpha = 222;

    void Start()
    {   
        StartCoroutine(DarkScreeen());
    }
    
    
    IEnumerator DarkScreeen()
    {
        Color panelColor = panel.color;
        panelColor.a = 1.0f;
        panel.color = panelColor;

        yield return new WaitForSeconds(3f);

        panelColor.a = panelAlpha / 255.0f;
        panel.color = panelColor;
        credit.isStart = true;
    }
}
