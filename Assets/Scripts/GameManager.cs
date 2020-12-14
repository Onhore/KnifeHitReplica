using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(GameUI))]
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                            var singleton = new GameObject("GameManager");
                            _instance = singleton.AddComponent<GameManager>();
                            //DontDestroyOnLoad(singleton);
                }
            }
                
            return _instance;
        }
    }

    #endregion

    [SerializeField] private int KnifeCount;

    [Header("Knife Spawn")]
    [SerializeField] private Vector2 KnifeSpawnPosition;
    [SerializeField] private GameObject KnifeObject;

    [Space]

    [Header("General Events")]
    public UnityEvent KnifeHit = new UnityEvent();
    public UnityEvent ThrowKnife = new UnityEvent();

    [Space]

    [Header("End of level")]
    [SerializeField] private float Duration;

    private GameUI gameUI;

    private void Awake()
    {
        gameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        gameUI.SceneFromDarkness();

        KnifeHit.AddListener(OnKnifeHit);
        ThrowKnife.AddListener(DecrementKnives);

        gameUI.SetInitialDisplayKnifeCount(KnifeCount);
        
    }

    private void OnKnifeHit()
    {
        if (KnifeCount <= 1)
        {
            StartCoroutine(RestartGame());
            return;
        }
        else
        {
            SpawnKnife();
        }
        
    }

    private void SpawnKnife()
    {
        KnifeCount--;
        Instantiate(KnifeObject, KnifeSpawnPosition, Quaternion.identity);
    }

    public void DecrementKnives() 
    {
        if (KnifeCount >=1)
            gameUI.DecrementDisplayKnifeCount();
    }

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(Duration);
        gameUI.SceneInDarkness();
    }
}
