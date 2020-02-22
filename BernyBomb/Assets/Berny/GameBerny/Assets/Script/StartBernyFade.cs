using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartBernyFade : MonoBehaviour
{
    public Text textBtn;
    public GameObject contBtn;
    public GameObject startcan;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        ShowMouseCursor();
    }

    public void Fade()
    {
        //gameObject.SetActive(false);
        Time.timeScale = 1f;
        HideMouseCursor();

        StartCoroutine(ExecuteAfterTime(1f));
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

        gameObject.GetComponentInChildren<Image>().DOFade(0f, 1f);
        gameObject.GetComponentInChildren<Text>().DOFade(0f, 1f);
        contBtn.GetComponent<Image>().DOFade(0f, 1f);
        textBtn.GetComponent<Text>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        startcan.SetActive(false);
    }
}
