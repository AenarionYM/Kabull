using UnityEngine;

public class Inventory : MonoBehaviour
{
    public void SlotCheck(GameObject slot)
    {
        if (slot.name == "Poczet")
        {
            return;
        }
    }
}
