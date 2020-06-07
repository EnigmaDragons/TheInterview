
public static class GameStateExtensions
{
    public static bool IsTriggered(this GameState g, string name) =>
        g.TransientTriggers.Contains(name) || g.PermanentTriggers.Contains(name);
}
