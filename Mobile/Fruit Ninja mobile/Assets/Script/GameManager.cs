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
    public Material capMaterial;
    public ParticleSystem fruitSplashEffectYellow;
    public ParticleSystem fruitSplashEffectRed;

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
    private int lifePoints;
    public Text scoreText;
    public Text highScoreText;
    public Image[] lifePointsImages;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject damageOverlay;

    private FruitBehaviour getFruit()
    {
        FruitBehaviour fb = fruit.Find(x => !x.isActive);

        if (fb == null)
        {
            int randomNmbr = Random.Range(0, 4);

            fb = Instantiate(fruitPrefab[randomNmbr]).GetComponent<FruitBehaviour>();
            fruit.Add(fb);
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
        lifePoints = 5;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        damageOverlay.SetActive(false);

        //Score
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("Score");
        highScoreText.text = "Best: " + highScore.ToString();

        //Set lives
        foreach (Image i in lifePointsImages)
        {
            i.enabled = true;
        }

        /*foreach (FruitBehaviour f in fruit)
        {
            Destroy(f.gameObject);
        }*/

        fruit.Clear();

        //Death
        deathMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoseLifePoint()
    {
        if (lifePoints > 0)
        {
            lifePoints--;
            lifePointsImages[lifePoints].enabled = false;
        }
        if (lifePoints == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isPaused = true;
        deathMenu.SetActive(true);
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "Best: " + highScore.ToString();
            PlayerPrefs.SetInt("Score", highScore);
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
    }

    private IEnumerator IncreaseDifficulity()
    {
        while (!maxSpeedReached)
        {
            yield return new WaitForSeconds(30);

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
            FruitBehaviour fb = getFruit();
            float randomX = Random.Range(-1.65f, 1.65f);

            fb.LauchFruit(Random.Range(1.85f, 2.75f), randomX, -randomX);

            lastSpawn = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            trail.gameObject.SetActive(true);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;

            trail.position = pos;
            Instantiate(trail.gameObject, transform);

            Collider2D[] thisFruitFrame = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y), LayerMask.GetMask("Fruit"));

            if ((Input.mousePosition - lastMousePosition).sqrMagnitude >= REQUIRED_SLICE_FORCE)
            {
                foreach (Collider2D c2 in thisFruitFrame)
                {
                    for (int i = 0; i < fruitCols.Length; i++)
                    {
                        if (c2 == fruitCols[i])
                        {
                            c2.GetComponent<FruitBehaviour>().Slice();
                            //Debug.Log(Input.mousePosition - lastMousePosition);
                            //Instantiate(cutFruitPrefab, pos, new Quaternion((Input.mousePosition - lastMousePosition).x, (Input.mousePosition - lastMousePosition).y, 1, 1));
                            //Instantiate(cutFruitPrefab, pos, c2.GetComponent<FruitBehaviour>().gameObject.transform.rotation);

                            GameObject victim = c2.GetComponent<FruitBehaviour>().gameObject;
                            
                            Debug.Log(victim.name);
                            if ((victim.name == "TomatoFruit(Clone)") || victim.name == "CarrotFruit(Clone)")
                            {
                                Instantiate(fruitSplashEffectRed, victim.transform);
                            }
                            else
                            {
                                Instantiate(fruitSplashEffectYellow, victim.transform);
                            }

                            Vector3 invertedPos = new Vector3(pos.y, pos.x, pos.z);
                            //GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, victim.transform.position, lastMousePosition, capMaterial);
                            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, victim.transform.position, invertedPos, capMaterial);

                            if (!pieces[1].GetComponent<Rigidbody>())
                                pieces[1].AddComponent<Rigidbody>();
                                
                            Destroy(pieces[1], 1);
                            victim.GetComponent<BoxCollider2D>().enabled = false; // IS THIS NECESSARY??
                        }
                    }
                    }
                }
                lastMousePosition = Input.mousePosition;
                fruitCols = thisFruitFrame;
            }
            else
            {
                //trailTest.SetActive(false);
                trail.gameObject.SetActive(false);
            }
        }
    }
