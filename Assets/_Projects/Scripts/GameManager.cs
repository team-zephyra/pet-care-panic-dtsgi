using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Setup Gameplay")]
    [SerializeField] private GameSetup gameSetup;
    [SerializeField] private Orders order;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameOver gameOverPanel;
    [SerializeField] private Scoring gameScore;
    [SerializeField] private UICountDown uiCountDown;
    private bool IsPaused = true;
    private int indexOrder = 0;
    private int petServicedCount = 0;

    [Header("Setup Pet")]
    public PetInitiate petInitiate;

    [SerializeField]private List<Pet> petList;
    [SerializeField]private List<OrderCard> orderList;

    //HideInInspector]
    public AudioSource audioSource;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(gameSetup == null)
        {
            FindObjectOfType<GameSetup>();  
        }

        if (audioSource == null)
        {
            audioSource = FindObjectOfType<GameAudioFX>().AudioSource;
        }

        TimerPause(true);
    }

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        // Count Down

        TimerPause(true);

        uiCountDown.StartCountDown();
        yield return new WaitForSeconds(4);

        TimerPause(false);

        gameTimer.StartGameTimer();

        // Initiate New Customer
        int idx = 0;
        int totalSpawn = gameSetup.addCustomerOrder.Length;

        do
        {
            if (IsPaused)
            {
                // wait a while before continue to avoid infinite loop
                yield return new WaitForEndOfFrame();
                continue;
            }

            //waiting 1 second in real time and increasing the timer value
            yield return new WaitForSecondsRealtime(gameSetup.addCustomerOrder[idx].timeToSpawn);
            if (!SpawnNewOrder())
            {
                if (!SpawnNewOrder())
                {
                    yield return new WaitForSeconds(1);
                    SpawnNewOrder();
                    idx++;
                    continue;
                }
            }
            else
            {
                idx++;
            }

        } while (idx < totalSpawn);
    }

    #region Spawn Order and Pet

    private bool SpawnNewOrder()
    {

        /* Checkin New Pet To Counter */
        int i = 0;
        do
        {
            Counter_Checkin cc = gameSetup.checkinCounters[i];
            if (!cc.HasPetObject())
            {
                // Add Pet and Spawn Pet
                PetCategory Tcat = gameSetup.addCustomerOrder[indexOrder].petType;
                OrderType Torder = gameSetup.addCustomerOrder[indexOrder].orderType;
                Pet p = cc.CheckInPet(petInitiate.GetPetsByCategory(Tcat));
                p.pet_order_index = indexOrder;

                // Add Order On UI
                OrderCard o = order.NewOrder(Tcat, Torder, indexOrder);

                //Set Order on Pet
                p.SetupPetNeeds(o.taskListSO, Torder);

                orderList.Add(o);
                petList.Add(p);

                indexOrder++;
                return true;
            }
            ++i;
        } while (i < gameSetup.checkinCounters.Count);

        return false;
    }

    /* Order System */

    public void UpdateOrderTask(OrderTaskCategory _cat, int _index)
    {
        OrderCard _orderCard;
        _orderCard = orderList[_index];

        switch (_cat)
        {
            case OrderTaskCategory.Bathing: _orderCard.taskList[0].TaskClear(); break;
            case OrderTaskCategory.Next: _orderCard.taskList[1].TaskClear(); break;
            case OrderTaskCategory.Drying: _orderCard.taskList[2].TaskClear(); break;
            case OrderTaskCategory.EarCleaning: _orderCard.taskList[0].TaskClear(); break;
            case OrderTaskCategory.HairCutting: _orderCard.taskList[1].TaskClear(); break;
            case OrderTaskCategory.NailTrimming: _orderCard.taskList[2].TaskClear(); break;
            case OrderTaskCategory.DayCareCheckin: _orderCard.taskList[0].TaskClear(); break;
            case OrderTaskCategory.DayCareFood: _orderCard.taskList[1].TaskClear(); break;
            case OrderTaskCategory.DayCareCheckout: _orderCard.taskList[2].TaskClear(); break;
        }

        petList[_index].UpdateTaskComplete(_cat);
    }


    #endregion

    #region Pause Settings

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        OnGamePaused(true);
    }

    public void ResumeGame()
    {
        OnGamePaused(false);
        pausePanel.SetActive(false);
    }

    private void OnGamePaused(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        TimerPause(_pause);
    }

    private void TimerPause(bool _pause)
    {
        IsPaused = _pause;
        gameTimer.pauseTimer = _pause;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("L_MainMenu");
    }
    #endregion

    #region Game Start & Game Over
    // Move To GameStart
    //private IEnumerator OnGameStart()
    //{
    //    TimerPause(true);

    //    uiCountDown.StartCountDown();
    //    yield return new WaitForSeconds(3);

    //    TimerPause(false);

    //    gameTimer.StartGameTimer();
        
    //}

    public void OnGameOver()
    {
        OnGamePaused(true);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        if(gameTimer.Timer > 0)
        {
            uiCountDown.ShowFinish();
        }
        else
        {
            uiCountDown.ShowTimesUp();
        }

        int cd = 3;

        while (cd > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            cd--;
        }

        gameOverPanel.GameOverTrigger();
    }

    #endregion

    #region Scoring

    public void CheckoutPet(int _score, int _petIndex)
    {
        gameScore.UpdateScore(_score);

        

        GameOverCheck();
    }

    private void GameOverCheck()
    {
        petServicedCount += 1;
        if (petServicedCount == gameSetup.addCustomerOrder.Length)
        {
            OnGameOver();
        }
    }

    #endregion
}
