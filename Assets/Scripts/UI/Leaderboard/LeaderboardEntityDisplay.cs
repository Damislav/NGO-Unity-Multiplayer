using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using Unity.Netcode;

public class LeaderboardEntityDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] Color myColor;
    private FixedString32Bytes playerName;

    public ulong ClientId { get; private set; }
    public int Coins { get; private set; }

    public void Initialise(ulong clientId, FixedString32Bytes playerName, int coins)
    {
        ClientId = clientId;
        Coins = coins;
        this.playerName = playerName;

        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            displayText.color = myColor;
        }

        UpdateCoins(coins);
    }
    public void UpdateCoins(int coins)
    {
        Coins = coins;
        UpdateText();
    }

    public void UpdateText()
    {
        displayText.text = $"{transform.GetSiblingIndex() + 1}. {playerName} ({Coins})";
    }



}

