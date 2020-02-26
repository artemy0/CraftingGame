using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsObjectPool : MonoBehaviour
{
    [SerializeField] private ItemScenePresenter _itemPresenterTemplate;

    private List<ItemScenePresenter> _available = new List<ItemScenePresenter>();
    private List<ItemScenePresenter> _inUse = new List<ItemScenePresenter>();

    public ItemScenePresenter Get(IItem item)
    {
        ItemScenePresenter presenter = null;

        if(_available.Count == 0)
        {
            presenter = Instantiate(_itemPresenterTemplate);
            presenter.Present(item);
            presenter.PickedUp += () => Release(presenter); //событие которое вызовится когда элемент будет забран со сцены
        }
        else
        {
            presenter = _available[0];
            _available.Remove(presenter);
        }

        presenter.gameObject.SetActive(true);
        _inUse.Add(presenter);
        return presenter;
    }

    public void Release(ItemScenePresenter presenter)
    {
        if(_inUse.Remove(presenter) == false)
        {
            return;
        }

        presenter.gameObject.SetActive(false);
        _available.Add(presenter);
    }
}
