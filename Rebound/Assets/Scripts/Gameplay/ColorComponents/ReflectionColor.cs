using DG.Tweening;
using Zenject;

public class ReflectionColor : ObjectColor
{
    private float _glowMaxValue;
    [Inject] private ReflectionController _controller;

    private void OnEnable()
    {
        SetColor(1f);
    }

    public override void SetColor(float intensityModifyer)
    {
        currentColor = _controller.ReflectionState.ReflectionColor;
        objectMaterial.DOColor(currentColor * intensity, "_Color", 0.1f).SetLink(gameObject).Play();
    }

    public override void ActivateGlowEffect(bool isActive)
    {
        _glowMaxValue = isActive ? 1f : 0.5f;
        DOTween.Sequence()
            .Append(objectMaterial.DOFloat(_glowMaxValue, "_GlowValue", 0.05f))
            .Append(objectMaterial.DOFloat(0f, "_GlowValue", 0.05f)).SetLink(gameObject);            
    }
}
