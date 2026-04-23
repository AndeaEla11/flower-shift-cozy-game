using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform counterPosition;
    [SerializeField] private Transform exitPosition;
    [SerializeField] private GameObject[] customerPrefabs;

    void Start()
    {
        if (GameState.bouquetDelivered)
        {
            SpawnCustomerAtCounter();
        }
        else
        {
            SpawnCustomer();
        }
    }

    public void SpawnCustomer()
    {
        if (customerPrefabs == null || customerPrefabs.Length == 0)
        {
            return;
        }

        GameState.currentCustomerIndex = Random.Range(0, customerPrefabs.Length);

        GameObject NPC = Instantiate(customerPrefabs[GameState.currentCustomerIndex], spawnPosition.position, spawnPosition.rotation);

        WaypointManager waypointManager = NPC.GetComponent<WaypointManager>();
        waypointManager.wayPoints.Clear();
        waypointManager.wayPoints.Add(counterPosition);
        waypointManager.StartMoving();

        CustomerFlow flow = NPC.GetComponentInChildren<CustomerFlow>(); 
        flow.Initiate(this, waypointManager, exitPosition);
    }

    public void SpawnCustomerAtCounter()
    {
        if (customerPrefabs == null || customerPrefabs.Length == 0)
        {
            return;
        }

        if (GameState.currentCustomerIndex < 0 || GameState.currentCustomerIndex >= customerPrefabs.Length)
        {
            return;
        }

        GameObject NPC = Instantiate(customerPrefabs[GameState.currentCustomerIndex], counterPosition.position, counterPosition.rotation);

        WaypointManager waypointManager = NPC.GetComponent<WaypointManager>();
        waypointManager.wayPoints.Clear();

        CustomerFlow flow = NPC.GetComponentInChildren<CustomerFlow>();
        flow.Initiate(this, waypointManager, exitPosition);
    }
}
