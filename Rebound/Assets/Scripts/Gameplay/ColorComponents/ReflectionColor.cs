
public class ReflectionColor : ObjectColor
{
    private void OnEnable()
    {
        SetColor(1f);
    }

    public override void SetColor(float intensityModifyer)//reflection
    {
        currentColor = ReflectionController.ReflectionState.ReflectionColor;
        objectMaterial.SetColor("_Color", ReflectionController.ReflectionState.ReflectionColor * intensity);//постепенно
    }
}
