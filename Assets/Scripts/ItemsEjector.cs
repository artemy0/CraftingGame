using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsEjector : MonoBehaviour
{
    [SerializeField] private ItemsObjectPool _pool; //объектный пул здесь не обязателен
    [SerializeField] private float _range;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    public void EjectFromPool(IItem item, Vector3 screenPosition, Vector3 direction)
    {
        ItemScenePresenter presenter = _pool.Get(item);

        Vector3 position = GetWorldEjectionPosition(screenPosition);

        presenter.transform.position = position;

        Vector3 target = position + (direction.normalized * _range);

        presenter.gameObject
            .AddComponent<MovingAlongLine>()
            .StartMoving(position, target, 1)
            .RemoveWhenFinished();
    }

    private Vector3 GetWorldEjectionPosition(Vector3 screenPosition)
    {
        return (Vector2)_camera.ScreenToWorldPoint(screenPosition); //Vector2 из-за того что при конвертации позийии z равнялась -10 и камера этот объект не видела, п при приведении к Vector2 z равняется 0
    }
}
