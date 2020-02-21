using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveDownWon : MonoBehaviour
{
    public GameObject wincanv;
    public PressM pressM;
    public Image backgr;
    public Image carrotimg;
    public Text wontext;
    public Text Mtext;
    bool tomove = false;

    // Update is called once per frame
    void Update()
    {
        if (tomove == true)
        {
            gameObject.transform.DOMoveY(gameObject.transform.position.y - 1, 2.5f);

            StartCoroutine(ExexuteAfterTime(5f));
        }
    }

    public void setmove()
    {
        tomove = true;
    }

    IEnumerator ExexuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        wincanv.SetActive(true);
        backgr.GetComponent<Image>().DOFade(1f, 1.5f);
        carrotimg.GetComponent<Image>().DOFade(1f, 1.5f);
        wontext.GetComponent<Text>().DOFade(1f, 1.5f);
        Mtext.GetComponent<Text>().DOFade(1f, 1.5f);
        pressM.setcanv();
    }
}
