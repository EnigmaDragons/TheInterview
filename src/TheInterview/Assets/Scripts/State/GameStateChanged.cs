public sealed class GameStateChanged
{
    public GameState State { get; set; }

    public GameStateChanged(GameState state) => State = state;
}
