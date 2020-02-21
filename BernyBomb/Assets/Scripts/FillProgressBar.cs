using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FillProgressBar : MonoBehaviour
{
    float filled;

    private void Start()
    {
        GetComponent<Image>().fillAmount = 0;
        filled = GetComponent<Image>().fillAmount;
    }

    public void FillUp()
    {
        GetComponent<Image>().DOFillAmount(filled + 0.2f, 1f);
        filled += 0.2f;

        FindObjectOfType<AudioManager>().Play("bar");
    }
}
