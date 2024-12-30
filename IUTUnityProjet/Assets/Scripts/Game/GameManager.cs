using TMPro; // Importez TextMeshPro namespace
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI coinCounterText; // Référence à un TextMeshProUGUI
    private int coinCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        coinCounterText.text = "Coins: " + coinCount;
    }
}
