using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallForceController : MonoBehaviour
{
    private Rigidbody _body = null;
    
    [Header("Direction Config")]
    [SerializeField] private GameObject marker;
    
    [Header("Force Config")]
    [SerializeField] private float incrementalForceStep = 0f;
    [SerializeField] private float force = 0f;
    [SerializeField] private float maxForce = 100f;
    private float minForce = 0f;

    private bool _canFireTheBall = false;
    private bool _isCharging = false;
    private bool _isAscending = false;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateForce(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_isCharging || !_canFireTheBall) return;
        Vector3 forward = marker?.transform.forward ?? transform.forward;
        _body.AddForce(forward * force, ForceMode.Impulse);
        force = 0f;
        _canFireTheBall = _isAscending = false;
    }

    
    private void UpdateForce(float elapsed)
    {
        if (Input.GetButtonDown("Fire1"))
            _canFireTheBall = true;

        _isCharging = _canFireTheBall && Input.GetButton("Fire1");
        if (!_isCharging) return;
        
        float newForceValue = 0f;
        float forceStep = incrementalForceStep * elapsed;
        newForceValue = _isAscending ? -forceStep : forceStep;

        force += newForceValue;
        force = Mathf.Clamp(force , minForce, maxForce);
            
        if(force >= maxForce)
            _isAscending = true;
        else if (force <= minForce)
            _isAscending = false;
    }
}
