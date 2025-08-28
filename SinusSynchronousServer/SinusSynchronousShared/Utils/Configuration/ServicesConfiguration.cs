using System.Text;

namespace SinusSynchronousShared.Utils.Configuration;

public class ServicesConfiguration : SinusConfigurationBase
{
    public string DiscordBotToken { get; set; } = string.Empty;
    public ulong? DiscordChannelForMessages { get; set; } = null;
    public ulong? DiscordChannelForCommands { get; set; } = null;
    public ulong? DiscordChannelForBotLog { get; set; } = null!;
    public ulong? DiscordRoleRegistered { get; set; } = null!;
    public bool KickNonRegisteredUsers { get; set; } = false;
    public Dictionary<ulong, string> VanityRoles { get; set; } = new Dictionary<ulong, string>();
    public int UidLength { get; set; } = 10;
    public bool LockRegistrationToRole { get; set; } = false;
    public ulong? DiscordRegistrationRole { get; set; } = null!;
    public int SecondaryUIDLimit { get; set; } = 5;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"{nameof(DiscordBotToken)} => {DiscordBotToken}");
        sb.AppendLine($"{nameof(DiscordChannelForMessages)} => {DiscordChannelForMessages}");
        sb.AppendLine($"{nameof(DiscordChannelForCommands)} => {DiscordChannelForCommands}");
        sb.AppendLine($"{nameof(DiscordRoleRegistered)} => {DiscordRoleRegistered}");
        sb.AppendLine($"{nameof(KickNonRegisteredUsers)} => {KickNonRegisteredUsers}");
        sb.AppendLine($"{nameof(UidLength)} => {UidLength}");
        sb.AppendLine($"{nameof(LockRegistrationToRole)} => {LockRegistrationToRole}");
        sb.AppendLine($"{nameof(DiscordRegistrationRole)} => {DiscordRegistrationRole}");
        foreach (var role in VanityRoles)
        {
            sb.AppendLine($"{nameof(VanityRoles)} => {role.Key} = {role.Value}");
        }
        return sb.ToString();
    }
}