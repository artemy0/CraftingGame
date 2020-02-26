using UnityEngine;

public class MovingAlongLine : MonoBehaviour
{
    private float _startTime;
    private float _duration;
    private Vector3 _from, _to;

    private bool _started = false;
    private bool _removeWhenFinished;

    public MovingAlongLine StartMoving(Vector3 from, Vector3 to, float duration)
    {
        _from = from;
        _to = to;
        _duration = duration;

        _started = true;
        _startTime = Time.time;

        return this;
    }

    private void Update()
    {
        if (_started)
        {
            var progress = (Time.time - _startTime) / _duration;

            transform.position = Vector3.Lerp(_from, _to, progress);

            if(progress >= 1 && _removeWhenFinished)
            {
                Destroy(this);
            }
        }
    }

    public void RemoveWhenFinished()
    {
        _removeWhenFinished = true;
    }
}
