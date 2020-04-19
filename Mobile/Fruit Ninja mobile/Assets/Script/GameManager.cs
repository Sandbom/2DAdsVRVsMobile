using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Adverty;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    public GameObject[] fruitPrefab;
    public GameObject cutFruitPrefab;
    public Transform trail;
    public GameObject trailTest;
    public Material capMaterialYellow;
    public Material capMaterialRed;

    //Splash effects
    public ParticleSystem fruitSplashEffectYellow;
    public ParticleSystem fruitSplashEffectRed;

    //Floating text
    public Text streakText;
    public GameObject floatingTextPrefabYellow;
    public GameObject floatingTextPrefabOrange;
    public GameObject floatingTextPrefabRed;
    public GameObject floatingTextPrefabPurple;
    public GameObject multiplier2x;
    public GameObject multiplier3x;
    public GameObject multiplier4x;
    public GameObject multiplier6x;

    //Fruit spawning & control
    private List<FruitBehaviour> fruit = new List<FruitBehaviour>();
    private float lastSpawn;
    private float deltaSpawn = 1.0f;
    private Collider2D[] fruitCols;
    private Vector3 lastMousePosition;
    private const float REQUIRED_SLICE_FORCE = 0;
    private bool isPaused = false;
    private bool maxSpeedReached = false;

    //UI
    private int score;
    private int highScore;
    private int streak;
    private int pointMultiplier;
    private float time;
    public Text scoreText;
    public Text highScoreText;
    public GameObject pauseMenu;
    public GameObject deathMenuContinue;
    public GameObject deathMenuGameOver;
    public GameObject damageOverlay;
    public Text timeText;
    public Text deathScoreText;
    public Text levelText;

    private FruitBehaviour getFruit()
    {
        FruitBehaviour fb = fruit.Find(x => !x.isActive);

        if (fb == null)
        {
            int randomNmbr = Random.Range(1, 11);

            if (randomNmbr < 10) // 90% chance to spawn "ordinary fruit"
            {
                int fruitIndex = Random.Range(0, 4);
                fb = Instantiate(fruitPrefab[fruitIndex]).GetComponent<FruitBehaviour>();
                fruit.Add(fb);
            }
            else
            {
                fb = Instantiate(fruitPrefab[4]).GetComponent<FruitBehaviour>();
                fruit.Add(fb);
            }
        }

        return fb;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        score = 0;
        streak = 0;
        pointMultiplier = 1;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        damageOverlay.SetActive(false);

        //Score
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("Score");
        highScoreText.text = "Best: " + highScore.ToString();

        fruit.Clear();

        //Death
        deathMenuContinue.SetActive(false);
        deathMenuGameOver.SetActive(false);

        multiplier2x.SetActive(false);
        multiplier3x.SetActive(false);
        multiplier4x.SetActive(false);
        multiplier6x.SetActive(false);

        if (SoundManager.Instance.isFirstScenePlayed())
        {
            levelText.text = "Level: 2/2";
        }
        else
        {
            levelText.text = "Level: 1/2";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoseLifePoint()
    {
        streak = 0;
        pointMultiplier = 1;

        multiplier2x.SetActive(false);
        multiplier3x.SetActive(false);
        multiplier4x.SetActive(false);
        multiplier6x.SetActive(false);
    }

    public void Death()
    {
        isPaused = true;
        //deathMenuContinue.SetActive(true);
        //deathScoreText.text = "Your score was:" + "\n" + score;

        SoundManager.Instance.addScenePlayed();

        if (SoundManager.Instance.isFirstScenePlayed())
        {
            deathMenuContinue.SetActive(true);
        }
        else
        {
            deathMenuGameOver.SetActive(true);
        }
    }

    public void goToNextLevel()
    {
        int startIndex = SoundManager.Instance.getStartSceneIndex();

        if (startIndex == 2)
        {
            SceneManager.LoadScene(3);
        }
        else if (startIndex == 3)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("Something has gone wrong!! with scenestartIndex");
        }
    }

    public IEnumerator Countdown(float timeValue = 180)
    {
        time = timeValue;
        while (time > 0)
        {
            int minutesRemaining = (int) time / 60;
            timeText.text = "Time left: "+ minutesRemaining + ":" + time%60;
            yield return new WaitForSeconds(1.0f);
            time--;
        }
        if (time <= 0)
        {
            timeText.text = "Time left: 0:0";
            Death();
        }
    }

    public void AddScore(int scoreAmount)
    {
        streak++;

        if (streak == 10)
        {
            Debug.Log(streak);
            ShowFloatingText(10);
            pointMultiplier = 2;
            multiplier2x.SetActive(true);
        }
        else if (streak == 20)
        {
            ShowFloatingText(20);
            pointMultiplier = 3;
            multiplier2x.SetActive(false);
            multiplier3x.SetActive(true);
        }

        else if (streak == 40)
        {
            ShowFloatingText(40);
            pointMultiplier = 4;
            multiplier3x.SetActive(false);
            multiplier4x.SetActive(true);
        }

        else if (streak == 60)
        {
            ShowFloatingText(60);
            pointMultiplier = 6;
            multiplier4x.SetActive(false);
            multiplier6x.SetActive(true);
        }

        score += scoreAmount*pointMultiplier;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "Best: " + highScore.ToString();
            PlayerPrefs.SetInt("Score", highScore);
        }
    }

    void ShowFloatingText(int streakNumber)
    {

        if (streakNumber == 10)
        {
            var streakText = Instantiate(floatingTextPrefabYellow);
            streakText.GetComponent<TextMesh>().text = "10 Streak!\n2x Points";
        }

        else if (streakNumber == 20)
        {
            var streakText = Instantiate(floatingTextPrefabOrange);
            streakText.GetComponent<TextMesh>().text = "20 Streak!\n3x Points";
        }

        else if (streakNumber == 40)
        {
            var streakText = Instantiate(floatingTextPrefabRed);
            streakText.GetComponent<TextMesh>().text = "4 Streak!\n4x Points";
        }

        else if (streakNumber == 60)
        {
            var streakText = Instantiate(floatingTextPrefabPurple);
            streakText.GetComponent<TextMesh>().text = "60 Streak!\n6x Points";
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        AdvertySDK.Init();
        fruitCols = new Collider2D[0];
        NewGame();
        StartCoroutine(IncreaseDifficulity());
        StartCoroutine(Countdown());
        Debug.Log("Starting new game");
    }

    private IEnumerator IncreaseDifficulity()
    {
        while (!maxSpeedReached)
        {
            yield return new WaitForSeconds(40);
            deltaSpawn -= 0.1f;
            Debug.Log("increasing speed!" + " speed is now: " + deltaSpawn);
            if (deltaSpawn <= 0.4f)
            {
                maxSpeedReached = true;
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (Time.time - lastSpawn > deltaSpawn)
        {
            int amountOfFruit = Random.Range(0, 10);

            if(amountOfFruit > 3)
            {
                FruitBehaviour fb = getFruit();
                float randomX = Random.Range(-1.65f, 1.65f);

                fb.LauchFruit(Random.Range(1.85f, 2.75f), randomX, -randomX);

                lastSpawn = Time.time;
            }
            else if(amountOfFruit == 2 || amountOfFruit == 1)
            {
                FruitBehaviour fb1 = getFruit();
                FruitBehaviour fb2 = getFruit();
                float randomX1 = Random.Range(-1.65f, 1.65f);
                float randomX2 = Random.Range(-1.65f, 1.65f);

                fb1.LauchFruit(Random.Range(1.85f, 2.75f), randomX1, -randomX1);
                fb2.LauchFruit(Random.Range(1.85f, 2.75f), randomX2, -randomX2);

                lastSpawn = Time.time;
            }
            else if(amountOfFruit == 0)
            {
                FruitBehaviour fb1 = getFruit();
                FruitBehaviour fb2 = getFruit();
                FruitBehaviour fb3 = getFruit();

                float randomX1 = Random.Range(-1.65f, 1.65f);
                float randomX2 = Random.Range(-1.65f, 1.65f);
                float randomX3 = Random.Range(-1.65f, 1.65f);

                fb1.LauchFruit(Random.Range(1.85f, 2.75f), randomX1, -randomX1);
                fb2.LauchFruit(Random.Range(1.85f, 2.75f), randomX2, -randomX2);
                fb3.LauchFruit(Random.Range(1.85f, 2.75f), randomX3, -randomX3);


                lastSpawn = Time.time;

            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;
            trail.position = pos;
            //trail.gameObject.GetComponent<TrailRenderer>().time = 0.1f;
            trail.gameObject.SetActive(true);

            Collider2D[] thisFruitFrame = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Fruit"));
            if ((Input.mousePosition - lastMousePosition).sqrMagnitude >= REQUIRED_SLICE_FORCE)
            {
                foreach (Collider2D c2 in thisFruitFrame)
                {
                    c2.GetComponent<FruitBehaviour>().Slice();
                    GameObject victim = c2.GetComponent<FruitBehaviour>().gameObject;

                    if ((victim.name == "TomatoFruit(Clone)") || victim.name == "CarrotFruit(Clone)")
                    {
                        Instantiate(fruitSplashEffectRed, victim.transform);
                        Vector3 invertedPos = new Vector3(pos.y, pos.x, pos.z);
                        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, victim.transform.position, invertedPos, capMaterialRed);

                        if (!pieces[1].GetComponent<Rigidbody>())
                            pieces[1].AddComponent<Rigidbody>();

                        Destroy(pieces[1], 1);
                        victim.GetComponent<BoxCollider2D>().enabled = false; // IS THIS NECESSARY??
                    }
                    else if ((victim.name == "BananaFruit(Clone)") || victim.name == "PineappleFruit(Clone)")
                    {
                        Instantiate(fruitSplashEffectYellow, victim.transform);
                        Vector3 invertedPos = new Vector3(pos.y, pos.x, pos.z);

                        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, victim.transform.position, invertedPos, capMaterialYellow);

                        if (!pieces[1].GetComponent<Rigidbody>())
                            pieces[1].AddComponent<Rigidbody>();

                        Destroy(pieces[1], 1);
                        victim.GetComponent<BoxCollider2D>().enabled = false; // IS THIS NECESSARY??
                    }
                    else //Fruit sliced is a bomb, make player lose HP
                    {
                        LoseLifePoint();
                        Camera.main.GetComponent<ShakeEffect>().ShakeCamera(0.7f,0.15f);
                        Destroy(victim.gameObject);
                        score = score - 5;
                        scoreText.text = score.ToString();
                    }
                    }
                }
                lastMousePosition = Input.mousePosition;
                fruitCols = thisFruitFrame;
        }
            else
            {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;
            trail.position = pos;
            //trail.gameObject.GetComponent<TrailRenderer>().time = 0;
            trail.gameObject.SetActive(false);
        }
        }
    }
