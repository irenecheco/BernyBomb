using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickIObj : MonoBehaviour, IPointerDownHandler
{
    public Transform Dest;
    public GameObject toPut;
    public GameObject InventoryGameObject;
    public GameObject Plug_Slot;
    public PlayerMovementEasy2 plmov;
    public FillProgressBar fillBar;
    Color mycol = new Color32(255, 255, 255, 255);
    bool plugged = false;

    private void Start()
    {
        Dest.position = new Vector3(Dest.position.x, Dest.position.y + 0.01f, Dest.position.z - 0.04f);
    }

    private void Update()
    {
        plugged = plmov.RetPlugged();

        if(plugged == true && Plug_Slot.gameObject.GetComponent<Image>().color == mycol)
        {
            Plug_Slot.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            fillBar.FillUp();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((gameObject.name == "Coin_Slot" && plugged == true) || gameObject.name != "Coin_Slot")
        {
            if (gameObject.GetComponent<Image>().color == mycol && Dest.childCount == 0)
            {
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                InventoryGameObject.SetActive(false);
                HideMouseCursor();
                plmov.playTito();
                toPut.SetActive(true);
                plmov.NoCollision(toPut);
                toPut.GetComponent<Rigidbody>().useGravity = false;
                if (gameObject.name == "Display_Slot" || gameObject.name == "Cassa_Slot")
                {
                    toPut.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
                }
                else
                {
                    toPut.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f);
                }
                toPut.transform.parent = GameObject.Find("Destination").transform;
                toPut.transform.position = new Vector3(Dest.position.x, Dest.position.y, Dest.position.z);
            }
        }        
    }
    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
