using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> keys = new List<string>();
    private int coins = 0;
    public void AddKey(string keyID)
    {
        if (!keys.Contains(keyID))
        {
            keys.Add(keyID);
            Debug.Log("Added key to inventory: " + keyID);
        }
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }

    // New method to get the keys list for UI display
    public List<string> GetKeys()
    {
        return new List<string>(keys); // Return a copy of the list
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public int GetCoins()
    {
        return coins;
    }
}