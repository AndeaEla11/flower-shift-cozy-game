using UnityEngine;

public class OrderValidator
{
    public static void ValidateOrder(OrderData order, BouquetBuilder bouquet)
    {
        int placedFlowerCount = bouquet.GetCount();
        int requiredFlowerCount = order.requiredTotalFlowers;

        int matchingTypeCount = 0;
        int matchingColourCount = 0;

        for (int i = 0; i < bouquet.placedFlowers.Count; i++)
        {
            GameObject flower = bouquet.placedFlowers[i];
            FlowerData flowerData = flower.GetComponent<FlowerData>();

           if (flowerData == null)
            {
                continue;
            }
            if (!string.IsNullOrEmpty(order.requiredFlowerType))
            {
                if (flowerData.flowerType == order.requiredFlowerType)
                {
                    matchingTypeCount++; 
                }
            }
            if (!string.IsNullOrEmpty(order.requiredFlowerColour))
            {
                if (flowerData.flowerColour == order.requiredFlowerColour)
                {
                    matchingColourCount++;
                }
            }
        }

        bool totalCorrect = placedFlowerCount >= requiredFlowerCount;

        bool typeCorrect = true;
        if (!string.IsNullOrEmpty(order.requiredFlowerType))
        {
            typeCorrect = matchingTypeCount >= order.requiredFlowerTypeCount;
        }

        bool colourCorrect = true;
        if (!string.IsNullOrEmpty(order.requiredFlowerColour))
        {
            colourCorrect = matchingColourCount >= order.requiredFlowerColourCount;
        }

        bool pass = totalCorrect && typeCorrect && colourCorrect;
        GameState.isCorrect = pass;

        GameState.placed = placedFlowerCount;
        GameState.required = requiredFlowerCount;

        string shortMessage = "";
        string feedback = "";

        if (totalCorrect)
        {
            feedback += "You included enough number of flowers.\n";
        }
        else
        {
            feedback += "You placed " + placedFlowerCount + " flowers, but the order asked for " + requiredFlowerCount + ".\n";
        }

        if (!string.IsNullOrEmpty(order.requiredFlowerType))
        {
            if (typeCorrect)
            {
                feedback += "You included enough " + order.requiredFlowerType + " flowers.\n";
            }
            else
            {
                feedback += "The bouquet needed at least " + order.requiredFlowerTypeCount + " " + order.requiredFlowerType + " flowers, but you placed " + matchingTypeCount + ".\n";
            }
        }

        if (!string.IsNullOrEmpty(order.requiredFlowerColour))
        {
            if (colourCorrect)
            {
                feedback += "You included enough " + order.requiredFlowerColour + " flowers.\n";
            }
            else
            {
                feedback += "The bouquet needed at least " + order.requiredFlowerColourCount + " " + order.requiredFlowerColour + " flowers, but you placed " + matchingColourCount + ".\n";
            }
        }

        if (pass)
        {
            string[] positiveResponses = {"That's perfect!", "Lovely, thank you!", "This is just what I wanted!", "Beautiful bouquet, thank you!"};

            int randomIndex = Random.Range(0, positiveResponses.Length);
            shortMessage = positiveResponses[randomIndex];
        }
        else
        {
            string[] negativeResponses = {"Not quite, but thank you!", "Hmm, not exactly what I wanted, but thank you.", "Close, but not quite right.", "I appreciate it, but this isn't quite it."};

            int randomIndex = Random.Range(0, negativeResponses.Length);
            shortMessage = negativeResponses[randomIndex];
        }

        GameState.feedbackMessage = shortMessage + "\n\n" + feedback;
    }
}
