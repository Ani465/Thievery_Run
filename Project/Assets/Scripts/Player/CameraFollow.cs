
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 _offset;
    private float _smoothSpeed = 0.125f;
    private Vector3 _velocity = Vector3.zero;
    
    // Start is called before the first frame update
    private void Start()
    {
        _offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var desiredPosition = target.position + target.rotation * _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothSpeed);
        transform.LookAt(target, target.up);
    }
}
