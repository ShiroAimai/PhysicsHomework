using UnityEngine;

public class BallPositioningController : MonoBehaviour
{
    [Header("Lateral Movement")] 
    [SerializeField] private float lateralMovementStep = 0f;
    [SerializeField] private float maxLateralMovement = 0f;
    private float _originalLateralPosition = 0f;

    private void Start()
    {
        _originalLateralPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLateralMovement(Time.deltaTime);
    }
    
    private void UpdateLateralMovement(float elapsed)
    {
        Vector3 currentPosition = transform.position;
        float lateralPositionDirection= Input.GetAxis("Horizontal");
        float newLateralPosition = currentPosition.x + (lateralMovementStep * lateralPositionDirection * elapsed);
        
        currentPosition.x = Mathf.Clamp(newLateralPosition, _originalLateralPosition - maxLateralMovement,
            _originalLateralPosition + maxLateralMovement);
        transform.position = currentPosition;
    } 
}
