public class Managers : SingletonBase<Managers>
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Data Class
    private readonly TrickManager _trickManager = new();
    private readonly AuthManager _authManager = new();
    private readonly SoundManager _soundManager = new();
    private readonly UIManager _uiManager = new();
    private readonly ResourceManager _resourceManager = new();
    private readonly PoolManager _poolManager = new();
    private readonly LoadingSceneManager _loadingSceneManager = new();
    
    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public static TrickManager Trick => Instance._trickManager;
    public static AuthManager Auth => Instance._authManager;
    public static SoundManager Sound => Instance._soundManager;
    public static UIManager UI => Instance._uiManager;
    public static ResourceManager Resource => Instance._resourceManager;
    public static PoolManager Pool => Instance._poolManager;
    public static LoadingSceneManager LoadingScene => Instance._loadingSceneManager;
}