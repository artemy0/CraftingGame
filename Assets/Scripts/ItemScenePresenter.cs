using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScenePresenter : MonoBehaviour
{
    public event Action PickedUp;

    [SerializeField] private SpriteRenderer _renderer;

    public void Present(IItem item)
    {
        _renderer.sprite = item.UIIcon;
        gameObject.name = item.Name;
    }

    public void PickUp()
    {
        PickedUp?.Invoke();
    }
}
