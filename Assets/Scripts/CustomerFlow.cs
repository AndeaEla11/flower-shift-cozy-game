using UnityEngine;

public class CustomerFlow : MonoBehaviour
{
    private GameController gameController;
    private WaypointManager waypointManager;

    private Transform exitPoint;

    private bool waitingAtCounter = false;
    private bool leaving = false;

    private Animator animator;
    private OrderUI orderUI;

    public void Initiate(GameController controller, WaypointManager wm, Transform exit)
    {
        gameController = controller;
        waypointManager = wm;
        exitPoint = exit;
        animator = GetComponentInChildren<Animator>();
        orderUI = FindFirstObjectByType<OrderUI>();
    }

    void Update()
    {
        if(waypointManager == null || gameController == null)
        {
            return;
        }

        if (!waitingAtCounter && !leaving && HasArrived())
        {
            waitingAtCounter = true;
            OnArrivedAtCounter(); 
        }

        if (leaving && HasArrived())
        {
            OnArrivedAtExit();
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", waypointManager.isMoving);
        }
    }

    bool HasArrived()
    {
        return !waypointManager.isMoving;
    }

    void OnArrivedAtCounter()
    {
        Vector3 directionToPlayer = Camera.main.transform.position - transform.position;
        directionToPlayer.y = 0f;

        if (directionToPlayer != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }

        if (GameState.bouquetDelivered)
        {
            OnBouquetDelivered();
            return;
        }

        OrderManager.Instance.GenerateNewOrder();

        if (orderUI != null)
        {
            orderUI.ShowOrder(OrderManager.Instance.CurrentOrder);
        }
    }

    public void OnBouquetDelivered()
    {
        if (orderUI != null)
        {
            orderUI.HideOrder();
        }

        StartCoroutine(ShowFeedbackAfterDelay());
    }

    private System.Collections.IEnumerator ShowFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);

        FeedbackUI feedback = FindAnyObjectByType<FeedbackUI>();
        if (feedback != null)
        {
            bool isCorrect = GameState.isCorrect;
            if (isCorrect)
            {
                GameState.correctCount++;
            }
            else
            {
                GameState.incorrectCount++;
            }

            PlayFeedbackAnimation(isCorrect);

            feedback.ShowResults(GameState.required, GameState.placed);
            GameState.bouquetDelivered = false;
        }
    }

    void PlayFeedbackAnimation(bool isCorrect)
    {
        if (animator == null)
        {
            return;
        }

        if (isCorrect)
        {
            animator.SetTrigger("Clap");
        }
        else
        {
            animator.SetTrigger("Nod");
        }
    }

    public void OnGoodbyeClicked()
    {
        FeedbackUI feedback = FindFirstObjectByType<FeedbackUI>();
        if (feedback != null)
        {
            feedback.Hide();
        }

        leaving = true;
        waitingAtCounter = false;

        waypointManager.wayPoints.Clear();
        waypointManager.wayPoints.Add(exitPoint);
        waypointManager.StartMoving();
    }

    void EndDay()
    {
        if (orderUI != null)
        {
            orderUI.HideOrder();
        }

        EndDayUI endUI = FindAnyObjectByType<EndDayUI>();
        if (endUI != null)
        {
            endUI.ShowResults(GameState.correctCount, GameState.incorrectCount);
        }
    }

    void OnArrivedAtExit()
    {
        GameState.customersServed++;

        bool endDay = GameState.customersServed >= GameState.maxCustomersPerDay;

        Destroy(transform.root.gameObject);

        if (endDay)
        {
            EndDay();
        }
        else
        {
            gameController.SpawnCustomer();
        }
    }
}
