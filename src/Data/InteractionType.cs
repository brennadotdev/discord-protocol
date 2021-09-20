namespace Discord.Protocol.Data
{
    public enum InteractionType
    {
        PingPong = 1,
        ApplicationCommand = 2,
        MessageComponent = 3,
        ChannelMessageWithSource = 4,
        DeferredChannelMessageWithSource = 5,
        DeferredUpdateMessage = 6,
        UpdateMessage = 7,
    }
}