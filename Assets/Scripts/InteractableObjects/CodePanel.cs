
public class CodePanel : InteractableObject
{
    public override void Interacted()
    {
        UIManager.instance.ShowUI(UI.CODEPANEL);
    }
}
