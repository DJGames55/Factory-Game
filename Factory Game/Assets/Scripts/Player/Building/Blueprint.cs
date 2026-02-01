using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public Building buildingScript;
    private bool canPlace = false;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = false; 
        Debug.Log(collider); 
        Debug.Log("Ent");
        buildingScript.BlueprintStateChange(canPlace);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = true; 
        Debug.Log(collider); 
        Debug.Log("Ext");
        buildingScript.BlueprintStateChange(canPlace);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.isTrigger) return;
        canPlace = false; 
        Debug.Log(collider); 
        Debug.Log("Sty");
        buildingScript.BlueprintStateChange(canPlace);
    }
}
