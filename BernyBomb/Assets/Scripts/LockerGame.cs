using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LockerGame : MonoBehaviour
{
    public GameObject LockerUI;
    public GameObject UITito;
    public GameObject Tito;
    public GameObject Locker;
    public GameObject Lock;
    public GameObject Locker_final;
    public GameObject OpenL;
    public GameObject ClosedL;
    public PlayerMovementEasy2 plmov;

    string[] combination = new string[] { "up", "right", "up", "down", "right" };
    int[] inputs = new int[5];
    int i = -1;

    private void Start()
    {
        for(var j=0; j<5; j++)
        {
            transform.Find("Locker_Panel/Code_Grid/" + j + "/" + j).GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        }
        OpenL.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y) && LockerUI.activeSelf)
        {
            ShortcutLocker();
        }*/
        if (inputs[4] == 4)
        {
            DOTween.Sequence().Append(ClosedL.GetComponent<Image>().DOFade(0.0f, 1f)).Join(OpenL.GetComponent<Image>().DOFade(1.0f, 1f).SetDelay(0.4f));
            transform.Find("Locker_Panel/Code_Grid").gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("lock_o");
            StartCoroutine(ExecuteAfterTime(2f));
            inputs[4] = 0;
        }
    }

    public void PlayLock()
    {
        LockerUI.SetActive(true);
        plmov.stopTito();
        ShowMouseCursor();
        UITito.SetActive(false);
    }
    public void ExitPlayLock()
    {
        LockerUI.SetActive(false);
        plmov.playTito();
        HideMouseCursor();
        UITito.SetActive(true);
        for (var j = 0; j < 5; j++)
        {
            transform.Find("Locker_Panel/Code_Grid/" + j + "/" + j).GetComponent<Image>().color = new Color32(255, 255, 225, 0);
        }
        i = -1;
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

    public void LightsCombination(string direction)
    {
        if(i < 5)
        {
            if (combination[i] == direction)
            {
                transform.Find("Locker_Panel/Code_Grid/" + i + "/" + i).GetComponent<Image>().color = new Color32(0, 255, 5, 255);
                inputs[i] = i;
            }
            else
            {
                //transform.Find("Locker_Panel/Code_Grid/" + i + "/" + i).GetComponent<Image>().color = new Color32(255, 37, 0, 255);
                DOTween.Sequence().Append(transform.Find("Locker_Panel/Code_Grid/" + i + "/" + i).GetComponent<Image>().DOColor(new Color32(255, 37, 0, 255), 0.2f))
                    .Append(transform.Find("Locker_Panel/Code_Grid/0/0").GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 0.05f).SetDelay(0.5f))
                    .Join(transform.Find("Locker_Panel/Code_Grid/1/1").GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 0.05f))
                    .Join(transform.Find("Locker_Panel/Code_Grid/2/2").GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 0.05f))
                    .Join(transform.Find("Locker_Panel/Code_Grid/3/3").GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 0.05f))
                    .Join(transform.Find("Locker_Panel/Code_Grid/4/4").GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 0.05f));
                i = -1;
                FindObjectOfType<AudioManager>().Play("fail");
            }
        }
    }

    public void Increment()
    {
        i += 1;
    }

    private void ShortcutLocker()
    {
        DOTween.Sequence().Append(ClosedL.GetComponent<Image>().DOFade(0.0f, 0.5f)).Join(OpenL.GetComponent<Image>().DOFade(1.0f, 0.5f));
        transform.Find("Locker_Panel/Code_Grid").gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("lock_o");
        StartCoroutine(ExecuteAfterTime(2f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Locker.SetActive(false);
        Lock.SetActive(false);
        Locker_final.SetActive(true);
        ExitPlayLock();
    }
}
