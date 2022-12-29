using UnityEngine;

public class Choose3Slot : MonoBehaviour
{
    private void OnMouseUp()
    {
        Events.EventDrawCard.Invoke(GetComponent<Card>().GetCardType());
        Events.EventChoose3Close.Invoke();
    }   
}
