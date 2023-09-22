using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu instance;

    [Header("Title Screen Settings")]
    public GameObject titleScreen;
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

    [Header("Credits Page Settings")]
    public GameObject creditsPage;
    public float creditsPageAnimationTime = 0.5f;
    private bool _isOnCreditsPage;

    [Header("Exit Pop Up Settings")]
    public GameObject exitPopup;
    public float exitPopupAnimationTime = 1f;
    private bool _isExitPopupActive;

    [Header("Player Input Settings")]
    private InputManager inputManager;
    private PlayerInput playerInput;

    [Header("UI Management")]
    [HideInInspector] public EventSystem eventSystem;
    public List<UIPage> pages;
    private int currentPage;
    private int defaultPage;
    public GameObject clickEffect;


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
            CreateClickEffect();
        }

        if (_isOnCreditsPage && playerInput.UI.Back.triggered)
        {
            CloseCreditsPage();
        }

        if (_isExitPopupActive && playerInput.UI.Back.triggered)
        {
            CancelExitButton();
        }
    }

    public void OpenExitPopup()
    {
        exitPopup.SetActive(true);

        LeanTween.scale(exitPopup, Vector3.one, exitPopupAnimationTime)
            .setEaseOutBounce();

        _isExitPopupActive = true;
    }

    public void ConfirmExitButton()
    {
        Application.Quit();
    }

    public void CancelExitButton()
    {
        LeanTween.scale(exitPopup, Vector3.zero, exitPopupAnimationTime)
            .setEaseInBack()
            .setOnComplete(DisableExitPopup);

        _isExitPopupActive = false;
    }

    public void OpenCreditsPage()
    {
        creditsPage.SetActive(true);

        LeanTween.moveLocalY(creditsPage, 0f, creditsPageAnimationTime)
            .setOnComplete(DisableMainMenu);

        UIPage page = creditsPage.GetComponent<UIPage>();
        page.SetSelectedUIToDefault();

        _isOnCreditsPage = true;
    }

    public void CloseCreditsPage()
    {
        // Enable MainMenu
        mainMenu.SetActive(true);

        // Close Credits Page animation
        LeanTween.moveLocalY(creditsPage, -750f, creditsPageAnimationTime)
            .setOnComplete(DisableCreditsPage);

        _isOnCreditsPage = false;
    }

    public void CreateClickEffect()
    {
        if (clickEffect)
        {
            Instantiate(clickEffect, transform.position, Quaternion.identity, null);
        }
    }

    private void SetupPlayerInput()
    {
        inputManager = FindObjectOfType<InputManager>();
        playerInput = inputManager.GetPlayerInput();
        eventSystem = FindObjectOfType<EventSystem>();

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

        // Enable MainMenu
        mainMenu.SetActive(true);

        LeanTween.scale(mainMenu, new Vector3(1f, 1f, 1f), mainMenuAnimationTime)
            .setEaseOutBounce()
            .setOnComplete(MainMenuButtonAnimation);

        UIPage page = mainMenu.GetComponent<UIPage>();
        page.SetSelectedUIToDefault();
    }

    private void MainMenuButtonAnimation()
    {
        foreach (GameObject button in mainMenuButtons)
        {
            LeanTween.scale(button, new Vector3(0.3f, 0.3f, 0.3f), mainMenuButtonAnimationTime)
                .setEaseOutBounce();
        }

        LeanTween.scale(exitButton, new Vector3(0.5f, 0.5f, 0.5f), mainMenuButtonAnimationTime)
                .setEaseOutBounce()
                .setOnComplete(DisableTitleScreen);
    }

    private void DisableTitleScreen()
    {
        titleScreen.SetActive(false);
    }

    private void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    private void DisableCreditsPage()
    {
        creditsPage.SetActive(false);
    }

    private void DisableExitPopup()
    {
        exitPopup.SetActive(false);
    }
}
