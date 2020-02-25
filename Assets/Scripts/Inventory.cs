﻿using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<AssetItem> Items;
    [SerializeField] private InventoryItemPresenter _inventoryItemPresenter;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingParent;

    private void OnEnable()
    {
        Render(Items);
    }

    public void Render(List<AssetItem> items)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        items.ForEach(item =>
        {
            InventoryItemPresenter itemPresenter = Instantiate(_inventoryItemPresenter, _container);
            itemPresenter.Init(_draggingParent);
            itemPresenter.Render(item);

            itemPresenter.Ejecting += () => Destroy(itemPresenter.gameObject);
        });
    }
}
