using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartFade : MonoBehaviour
{
    public Text intro;
    public Text textBtn;
    public GameObject UITito;
    public GameObject contBtn;
    public Canvas StartCanvas;
    public GameObject StartCanvas1;
    public PlayerMovementEasy2 plmov;
    public GameObject progBar;

    // Start is called before the first frame update
    void Start()
    {
        //Fade();
        plmov.stopTito();
        ShowMouseCursor();
        UITito.SetActive(false);
        progBar.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Fade()
    {
        intro.DOFade(0.0f, 2.0f);
        //GetComponent<MeshRenderer>().material.DOFade(0.0f, 2.0f);
        DOTween.Sequence().Append(GetComponent<MeshRenderer>().material.DOFade(0.0f, 1.5f)).Append(transform.DOMoveZ(-5, 0.5f));
        contBtn.GetComponent<Image>().DOFade(0.0f, 2.0f);
        textBtn.DOFade(0.0f, 2.0f);
        plmov.playTito();
        HideMouseCursor();
        StartCoroutine(ExecuteAfterTime(1.6f));
        FindObjectOfType<AudioManager>().Fade("Theme");
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        UITito.SetActive(true);
        gameObject.SetActive(false);
        StartCanvas1.SetActive(false);
        //StartCanvas.GetComponent<Canvas>().sortingOrder = 0;
    }
}
