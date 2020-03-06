using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScatolinaGame : MonoBehaviour
{
    //public static bool GameIsPaused = false;
    public GameObject ScatolinaUI;
    public GameObject UITito;
    public GameObject Scatolina;
    public GameObject Ingranaggi;
    public GameObject Scatolina_final;
    public PlayerMovementEasy2 plmov;
    List<GameObject> list = new List<GameObject>();

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y) && ScatolinaUI.activeSelf)
        {
            ShortcutScatolina();
        }*/
    }

    public void ExitPlayBox()
    {
        ScatolinaUI.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;
        plmov.playTito();
        HideMouseCursor();
        UITito.SetActive(true);
    }

    public void PlayBox()
    {
        ScatolinaUI.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
        plmov.stopTito();
        ShowMouseCursor();
        UITito.SetActive(false);
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
    
    public void InList(GameObject nuovo)
    {
        if (!list.Contains(nuovo))
        {
            list.Add(nuovo);
            if(list.Count == 6)
            {
                Scatolina.SetActive(false);
                Ingranaggi.SetActive(false);
                Scatolina_final.SetActive(true);
                FindObjectOfType<AudioManager>().Play("box_o");
                StartCoroutine(ExecuteAfterTime(0.5f));
            }
        }
    }

    private void ShortcutScatolina()
    {
        Scatolina.SetActive(false);
        Ingranaggi.SetActive(false);
        Scatolina_final.SetActive(true);
        FindObjectOfType<AudioManager>().Play("box_o");
        StartCoroutine(ExecuteAfterTime(0.5f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        ExitPlayBox();
    }
}
