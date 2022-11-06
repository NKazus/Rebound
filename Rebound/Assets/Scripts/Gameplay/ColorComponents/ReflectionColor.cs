using DG.Tweening;

public class ReflectionColor : ObjectColor
{
    private float _glowMaxValue;
    private void OnEnable()
    {
        SetColor(1f);
    }

    public override void SetColor(float intensityModifyer)
    {
        currentColor = ReflectionController.ReflectionState.ReflectionColor;
        objectMaterial.DOColor(ReflectionController.ReflectionState.ReflectionColor * intensity, "_Color", 0.1f).Play();
    }

    public override void ActivateGlowEffect(bool isActive)
    {
        _glowMaxValue = isActive ? 1f : 0.5f;
        DOTween.Sequence()
            .Append(objectMaterial.DOFloat(_glowMaxValue, "_GlowValue", 0.05f))
            .Append(objectMaterial.DOFloat(0f, "_GlowValue", 0.05f));            
    }
}
