public class Managers : SingletonBase<Managers>
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Data Class
    private readonly TrickManager _trickManager = new();
    private readonly AuthManager _authManager = new();
    private readonly SoundManager _soundManager = new();
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public static TrickManager Trick => Instance._trickManager;
    public static AuthManager Auth => Instance._authManager;
    public static SoundManager Sound => Instance._soundManager;
}