using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseManager : MonoBehaviour
{
    public UnityEvent<int> OnPurchaseDone;

    [SerializeField] private int moneyOnStart;    

    private int money;

    public int Money => money;

    public void Initialize()
    {
        money = moneyOnStart;
    }

    public void PurchaseItems(List<ClothingItemSO> items)
    {
        List<ClothingItemSO> itemsToPurchase = new List<ClothingItemSO>();
        int totalCost = 0;
        for(int i = 0; i < items.Count; i++)
        {
            if (!GameplayManager.Instance.Inventory.PlayerOwnsItem(items[i]))
            {
                itemsToPurchase.Add(items[i]);
                totalCost += items[i].cost;
            }            
        }

        if (totalCost > money)
        {
            return;
        } 
        else
        {
            money -= totalCost;
            GameplayManager.Instance.Inventory.AddOwnedItems(itemsToPurchase);
            OnPurchaseDone?.Invoke(money);
        }
    }
}
