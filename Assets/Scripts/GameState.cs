using UnityEngine;

public static class GameState
{
    public static bool bouquetDelivered = false;
    public static int placed = 0;
    public static int required = 0;
    public static string feedbackMessage;
    public static int currentCustomerIndex = -1;
    public static int correctCount = 0;
    public static int incorrectCount = 0;
    public static int customersServed = 0;
    public static int maxCustomersPerDay = 5;
    public static bool isCorrect = false;

    public static void ResetDay()
    {
        correctCount = 0;
        incorrectCount = 0;
        customersServed = 0;
        bouquetDelivered = false;
    }

}
