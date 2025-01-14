public class SpeedModifier
{
    public float Modifier { get; private set; } = 1f; // 기본값은 1 (변경되지 않음)

    public void ApplyModifier(float modifier)
    {
        Modifier = modifier;
    }

    public void ResetModifier()
    {
        Modifier = 1f; // 초기값으로 리셋
    }
}