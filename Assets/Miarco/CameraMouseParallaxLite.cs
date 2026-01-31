using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Applies a very subtle, lerped camera rotation based on mouse position
/// to enhance parallax on main menu backgrounds.
/// </summary>
public class CameraMouseParallaxLite : MonoBehaviour
{
    [Header("Rotation Limits (Degrees)")]
    [Tooltip("Max yaw (left/right) in degrees.")]
    public float maxYaw = 1.5f;

    [Tooltip("Max pitch (up/down) in degrees.")]
    public float maxPitch = 1.0f;

    [Header("Smoothing")]
    [Tooltip("How fast the camera lerps to the target rotation.")]
    public float lerpSpeed = 6.0f;

    [Header("Input")]
    [Tooltip("If true, uses unscaled deltaTime (recommended for menus).")]
    public bool useUnscaledTime = true;

    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialRotation = transform.localRotation;
    }

    private void Update()
    {
        // Normalized mouse position in [0..1]
        Vector3 mouse = Mouse.current.position.value;
        float nx = 0.5f;
        float ny = 0.5f;

        if (Screen.width > 0) nx = Mathf.Clamp01(mouse.x / Screen.width);
        if (Screen.height > 0) ny = Mathf.Clamp01(mouse.y / Screen.height);

        // Remap to [-1..1] around center
        float x = (nx - 0.5f) * 2f;
        float y = (ny - 0.5f) * 2f;

        // Invert Y so moving mouse up pitches camera slightly down (feels natural)
        float targetYaw = x * maxYaw;
        float targetPitch = -y * maxPitch;

        Quaternion target = _initialRotation * Quaternion.Euler(targetPitch, targetYaw, 0f);

        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, 1f - Mathf.Exp(-lerpSpeed * dt));
    }

    /// <summary>
    /// Resets the camera instantly to its initial local rotation.
    /// </summary>
    public void ResetInstant()
    {
        transform.localRotation = _initialRotation;
    }
}
