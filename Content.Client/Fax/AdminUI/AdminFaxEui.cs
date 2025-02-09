using Content.Client.Eui;
using Content.Shared.Eui;
using Content.Shared.Fax;
using JetBrains.Annotations;

namespace Content.Client.Fax.AdminUI;

[UsedImplicitly]
public sealed class AdminFaxEui : BaseEui
{
    private readonly AdminFaxWindow _window;

    public AdminFaxEui()
    {
        _window = new AdminFaxWindow();
        _window.OnClose += () => SendMessage(new AdminFaxEuiMsg.Close());
        _window.OnFollowFax += uid => SendMessage(new AdminFaxEuiMsg.Follow(uid));
        _window.OnMessageSend += args => SendMessage(new AdminFaxEuiMsg.Send(args.uid, args.title,
                    args.stampedBy, args.message, args.stampSprite, args.stampColor));
    }

    public override void Opened()
    {
        _window.OpenCentered();
    }

    public override void Closed()
    {
        _window.Close();
    }

    public override void HandleState(EuiStateBase state)
    {
        if (state is not AdminFaxEuiState cast)
            return;
        _window.PopulateFaxes(cast.Entries);
    }
}
