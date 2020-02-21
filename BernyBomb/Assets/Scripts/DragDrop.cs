using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject itemDragged;

    public GameObject st;
    public GameObject end;
    public ScatolinaGame scatolinaGame;
    //private CanvasGroup canvasGroup;
    float distance;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        itemDragged = gameObject;
        //canvasGroup.blocksRaycasts = false;

        FindObjectOfType<AudioManager>().Play("interact");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        transform.position = Input.mousePosition;
        distance = Vector3.Distance(end.transform.position, Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        itemDragged = null;
        //canvasGroup.blocksRaycasts = true;
        if (distance>15)
        {
            transform.position = st.transform.position;
            FindObjectOfType<AudioManager>().Play("fail");
        }
        else
        {
            transform.position = end.transform.position;
            scatolinaGame.InList(end);
            FindObjectOfType<AudioManager>().Play("interact");
        };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
}
