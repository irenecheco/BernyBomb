using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Move_Knob : MonoBehaviour/*, IPointerDownHandler*/
{
    Vector2 startPos;
    public RectTransform Knob;
    public LockerGame lockerGame;

    private void Start()
    {
        startPos = Knob.GetComponent<RectTransform>().anchoredPosition;
    }

    public void BtnUp()
    {
        DOTween.Sequence().Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y + 55), 0.4f)).Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y), 0.2f).SetDelay(0.07f));
        lockerGame.Increment();
        lockerGame.LightsCombination("up");
        FindObjectOfType<AudioManager>().Play("interact");
    }

    public void BtnDown()
    {
        DOTween.Sequence().Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y - 55), 0.4f)).Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y), 0.2f).SetDelay(0.07f));
        lockerGame.Increment();
        lockerGame.LightsCombination("down");
        FindObjectOfType<AudioManager>().Play("interact");
    }

    public void BtnR()
    {
        DOTween.Sequence().Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x + 55, startPos.y), 0.4f)).Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y), 0.2f).SetDelay(0.07f));
        lockerGame.Increment();
        lockerGame.LightsCombination("right");
        FindObjectOfType<AudioManager>().Play("interact");
    }

    public void BtnL()
    {
        DOTween.Sequence().Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x - 55, startPos.y), 0.4f)).Append(Knob.GetComponent<RectTransform>().DOAnchorPos(new Vector2(startPos.x, startPos.y), 0.2f).SetDelay(0.07f));
        lockerGame.Increment();
        lockerGame.LightsCombination("left");
        FindObjectOfType<AudioManager>().Play("interact");
    }

}
