using UnityEngine;
using UnityEngine.UI;

public class InteractMsg : MonoBehaviour
{
    public GameObject InteractMessage;
    Text T;

   public void OpenMessagePanel(string text)
    {
        InteractMessage.SetActive(true);
        InteractMessage.GetComponentInChildren<Text>().text = text;
    }

    public void CloseMessagePanel()
    {
        InteractMessage.SetActive(false);
    }
}
