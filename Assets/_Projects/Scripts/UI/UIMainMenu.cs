using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [Header("Title Screen Settings")]
    public GameObject titleScreenBackground;
    public GameObject titleLogo;
    public float titleLogoAnimationTime = 1f;
    public GameObject titleText;
    public float titleTextAnimationTime = 1f;
    private bool _isOnTitleScreen;

    [Header("Main Menu Page Settings")]
    public GameObject mainMenu;
    public float mainMenuAnimationTime = 1f;
    public GameObject mainMenuBackground;
    public GameObject[] mainMenuButtons;
    public GameObject exitButton;
    public float mainMenuButtonAnimationTime = 1f;

    [Header("Player Input Settings")]
    private InputManager inputManager;
    private PlayerInput playerInput;


    // Start is called before the first frame update
    void Start()
    {
        StartupAnimation();
        SetupPlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOnTitleScreen && playerInput.UI.AnyKey.triggered)
        {
            MainMenuAnimation();
        }
    }

    private void SetupPlayerInput()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerInput = inputManager.GetPlayerInput();

        // Disable Player Input until animation is done
        playerInput.Disable();

        // Animation is done, enable Player Input
        Invoke("EnablePlayerInput", titleTextAnimationTime);

        // Set isOnTitleScreen boolean
        _isOnTitleScreen = true;
    }

    private void EnablePlayerInput()
    {
        playerInput.Enable();
    }

    private void StartupAnimation()
    {
        LeanTween.moveLocalX(titleLogo, 0f, titleLogoAnimationTime)
            .setEaseOutBounce();
        LeanTween.scale(titleText, new Vector3(1f, 1f, 1f), titleTextAnimationTime)
            .setDelay(titleLogoAnimationTime)
            .setEaseOutBounce();
    }

    private void MainMenuAnimation()
    {
        // Reset isOnTitleScreen boolean
        _isOnTitleScreen = false;

        LeanTween.scale(mainMenu, new Vector3(1f, 1f, 1f), mainMenuAnimationTime)
            .setEaseOutBounce()
            .setOnComplete(MainMenuButtonAnimation);
    }

    private void MainMenuButtonAnimation()
    {
        foreach (GameObject button in mainMenuButtons)
        {
            LeanTween.scale(button, new Vector3(0.3f, 0.3f, 0.3f), mainMenuButtonAnimationTime)
                .setEaseOutBounce();
        }

        LeanTween.scale(exitButton, new Vector3(0.5f, 0.5f, 0.5f), mainMenuButtonAnimationTime)
                .setEaseOutBounce();
    }
}
