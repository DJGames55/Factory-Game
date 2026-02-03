using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public Building buildingScript;
    public Collider placementCollider;
    public LayerMask blockingLayers;

    private bool canPlace;
    private void Awake()
    {
        if (placementCollider == null)
        {
            placementCollider = GetComponent<Collider>();
        }
    }

    public bool CheckPlacement()
    {
        Collider[] hits = Physics.OverlapBox(
            placementCollider.bounds.center,
            placementCollider.bounds.extents * 0.95f, // slightly smaller to avoid edge touching
            placementCollider.transform.rotation,
            blockingLayers,
            QueryTriggerInteraction.Ignore
        );

        canPlace = true;

        foreach (Collider hit in hits)
        {
            if (hit == placementCollider) continue; // ignore self
            canPlace = false;
            break;
        }

        buildingScript.BlueprintStateChange(canPlace);
        return canPlace;
    }
}
