using UnityEngine;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MarkerController : BaseBallController
{
    private GameObject _ball;
    [SerializeField] private GameObject marker;
    [SerializeField] private float markerDistanceFromBall = 3f;

    private Plane plane;
    private void Start()
    {
        _ball = gameObject;
        plane = new Plane(Vector3.up, _ball.transform.position);
    }

    void Update()
    {
        if (!_ball || !marker) return;
        
        Vector3 newMarkerDirection = FindNewMarkerDirection();
        marker.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(marker.transform.forward, newMarkerDirection,30f, 0f)); 
        marker.transform.position = _ball.transform.position + (newMarkerDirection * markerDistanceFromBall);
    }

    private Vector3 FindNewMarkerDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return plane.Raycast(ray, out var distance) ? (ray.GetPoint(distance) - _ball.transform.position).normalized : marker.transform.forward;
    }

    protected override void OnControllerStateChange(bool value)
    {
        marker.SetActive(value);
    }
}
