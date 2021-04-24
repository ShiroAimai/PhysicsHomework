
using UnityEngine;

public abstract class BaseBallController : MonoBehaviour
{
    private bool _isEnabled = true;

    public bool enabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled == value) return;
            _isEnabled = value;
            OnControllerStateChange(_isEnabled);
        }
    }

    protected virtual void OnControllerStateChange(bool value)
    {
    }
}