using UnityEngine;

public class FlowerPickup : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Camera worldCamera;
    [SerializeField] private LayerMask bouquetSurfaceMask;
    [SerializeField] private Transform bouquetBasePoint;
    [SerializeField] private float spawnYOffset = 0.01f;

    void Awake()
    {
        if (worldCamera == null)
        {
            worldCamera = Camera.main;
        }
    }

    void OnMouseDown()
    {
        if (spawnPrefab == null || worldCamera == null || bouquetBasePoint == null)
        {
            return;
        }

        GameObject newFlower = Instantiate(spawnPrefab, bouquetBasePoint.position + Vector3.up * spawnYOffset, spawnPrefab.transform.rotation);
        EditableFlower editableFlower = newFlower.GetComponent<EditableFlower>();
        if (editableFlower != null)
        {
            editableFlower.Setup(worldCamera, bouquetSurfaceMask, bouquetBasePoint);
            editableFlower.SetSelected(true);
        }

        BouquetBuilder builder = FindFirstObjectByType<BouquetBuilder>();
        if (builder != null)
        {
            builder.RegisterFlower(newFlower);
        }
    }
}