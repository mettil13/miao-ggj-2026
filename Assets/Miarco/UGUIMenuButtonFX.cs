using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Adds hover/press animations to a UGUI Button using DOTween:
/// - subtle scale on hover/press
/// - TextMeshPro glow boost on hover (via TMP material properties)
/// </summary>
[RequireComponent(typeof(Button))]
public class UGUIMenuButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [Tooltip("If null, will search in children.")]
    public TextMeshProUGUI label;

    [Header("Scale")]
    public float hoverScale = 1.05f;
    public float pressScale = 0.98f;
    public float hoverDuration = 0.12f;
    public float pressDuration = 0.06f;
    public Ease ease = Ease.OutQuad;

    [Header("Glow (TMP)")]
    [Tooltip("Enable glow animation for TMP material.")]
    public bool animateGlow = true;

    [Tooltip("Glow power when not hovered.")]
    public float glowPowerNormal = 0.0f;

    [Tooltip("Glow power when hovered.")]
    public float glowPowerHover = 0.35f;

    [Tooltip("Glow color when hovered (HDR-ish works if your pipeline supports it).")]
    public Color glowColorHover = new Color(1f, 0.55f, 0.12f, 1f);

    [Tooltip("Optional: also tint text color a bit on hover.")]
    public bool tintTextOnHover = true;

    public Color textColorNormal = Color.white;
    public Color textColorHover = new Color(1f, 0.92f, 0.75f, 1f);

    private RectTransform _rt;
    private Vector3 _baseScale;
    private bool _isHovered;
    private bool _isPressed;

    private Tween _scaleTween;

    // Glow handling
    private Material _runtimeMat;
    private Tween _glowTween;
    private float _glowPowerCurrent;

    // Shader property IDs (faster, safer)
    private static readonly int GlowPowerID = Shader.PropertyToID("_GlowPower");
    private static readonly int GlowColorID = Shader.PropertyToID("_GlowColor");

    public AudioSource hoverAudioSource, clickAudioSource;

    private void Awake()
    {
        _rt = transform as RectTransform;
        _baseScale = _rt.localScale;

        if (label == null)
            label = GetComponentInChildren<TextMeshProUGUI>(true);

        if (label != null)
        {
            // Create an instance material so we don't modify the shared font material
            _runtimeMat = Instantiate(label.fontMaterial);
            label.fontMaterial = _runtimeMat;

            // Initialize
            _glowPowerCurrent = glowPowerNormal;
            ApplyGlow(_glowPowerCurrent, glowColorHover);

            if (tintTextOnHover)
                label.color = textColorNormal;
        }
    }

    private void OnDisable()
    {
        _scaleTween?.Kill();
        _glowTween?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
        AnimateToState();

        hoverAudioSource?.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        _isPressed = false;
        AnimateToState();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        AnimateToState();

        clickAudioSource?.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        AnimateToState();
    }

    private void AnimateToState()
    {
        // ----- SCALE -----
        float targetScale = 1f;

        if (_isPressed) targetScale = pressScale;
        else if (_isHovered) targetScale = hoverScale;

        _scaleTween?.Kill();
        float dur = _isPressed ? pressDuration : hoverDuration;

        _scaleTween = _rt.DOScale(_baseScale * targetScale, dur)
            .SetEase(ease)
            .SetUpdate(true);

        // ----- GLOW -----
        if (label == null || _runtimeMat == null || !animateGlow)
            return;

        float targetGlow = _isHovered ? glowPowerHover : glowPowerNormal;

        _glowTween?.Kill();
        _glowTween = DOTween.To(() => _glowPowerCurrent, v =>
        {
            _glowPowerCurrent = v;
            ApplyGlow(_glowPowerCurrent, glowColorHover);
        },
            targetGlow, hoverDuration)
            .SetEase(ease)
            .SetUpdate(true);

        // Optional text color tint
        if (tintTextOnHover)
            label.DOColor(_isHovered ? textColorHover : textColorNormal, hoverDuration)
                 .SetEase(ease)
                 .SetUpdate(true);
    }

    private void ApplyGlow(float power, Color color)
    {
        if (_runtimeMat == null) return;

        // Not all TMP shaders expose glow; if yours doesn't, this simply won't do anything.
        if (_runtimeMat.HasProperty(GlowPowerID))
            _runtimeMat.SetFloat(GlowPowerID, power);

        if (_runtimeMat.HasProperty(GlowColorID))
            _runtimeMat.SetColor(GlowColorID, color);
    }
}
