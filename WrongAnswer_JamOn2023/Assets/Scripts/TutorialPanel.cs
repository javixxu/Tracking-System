using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class TutorialPanel : MonoBehaviour
{
    TMP_Text[] text;
    Image[] image;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentsInChildren<TMP_Text>();
        image = gameObject.GetComponentsInChildren<Image>();
        Invoke("Fade", 4.0f);
    }
    private void Fade()
    {
        foreach (TMP_Text item in text)
            item.DOFade(0, 1.5f);
        foreach (Image item in image)
            item.DOFade(0, 1.5f);
    }
}
