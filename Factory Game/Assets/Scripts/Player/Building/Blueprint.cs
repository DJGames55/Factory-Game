using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public Building buildingScript;
    private bool canPlace = false;

    public bool CheckPlacement()
    {
        Debug.Log(canPlace);
        return canPlace;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = false; 
        buildingScript.BlueprintStateChange(canPlace);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = true; 
        buildingScript.BlueprintStateChange(canPlace);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = false; 
        buildingScript.BlueprintStateChange(canPlace);
    }
}
