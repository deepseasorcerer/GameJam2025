using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance = null;
    public static MusicManager Instance
    {
        get { return _instance; }
    }
    public enum MusicIntensity
    {
        Low = 0,
        High = 1,
    }

    public MusicIntensity currentIntensity = MusicIntensity.Low;
    [SerializeField] private AudioSource MainMenuMusic;
    [SerializeField] private AudioSource[] GameMusic = new AudioSource[3];
    float timeSinceGameStarted = 0;

    private float gameMusicBpm = 120;
    private double songEndTime;
    private double timeSinceMusicStarted;
    private float fadeTime = 3f;

    private float volumnMax = .5f;

    private bool inMainGame = false;

    private Scene scene;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        scene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene2, LoadSceneMode mode)
    {
        if (scene2.name == "MainMenu")
        {
            MainMenuMusic.Play();
            inMainGame = false;
            StopGameMusic();
        }
        else if (scene2.name == "MainGame")
        {
            inMainGame = true;
        }
        else if (scene2.name == "VictoryScreen" || scene2.name == "DefeatScreen")
        {
            StopGameMusic();
        }
    }

    void Update()
    {
        if ((AudioSettings.dspTime >= songEndTime || !GameMusic[0].isPlaying) && inMainGame)
        {
            StartGameMusic();
            timeSinceMusicStarted = 0;
            SetMusicIntensity(currentIntensity);
        }
        timeSinceGameStarted += Time.deltaTime;
    }
    
    private void StopGameMusic()
    {
        for (int i = 0; i < GameMusic.Length; i++)
        {
            GameMusic[i].Stop();
        }
    }

    public void StartGameMusic()
    {
        MainMenuMusic.Stop();

        for (int i = 0; i < GameMusic.Length; i++)
        {
            if (!GameMusic[i].isPlaying)
            {
                GameMusic[i].loop = true; 
                GameMusic[i].Play(); 
            }
            GameMusic[i].volume = (i == (int)currentIntensity) ? volumnMax : 0; 
        }
    }

    public void SetMusicIntensity(MusicIntensity intensity)
    {
        if (currentIntensity == intensity)
            return;

        currentIntensity = intensity;

        StopAllCoroutines();

        for (int i = 0; i < GameMusic.Length; i++)
        {
            if (i == (int)intensity)
            {
                StartCoroutine(FadeInMusic(GameMusic[i])); 
            }
            else
            {
                StartCoroutine(FadeOutMusic(GameMusic[i])); 
            }
        }
    }


    private IEnumerator FadeOutMusic(AudioSource audioSource)
    {
        float initialVolume = audioSource.volume;

        for (float t = 0; t <= fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeTime);
            yield return null;
        }

        audioSource.volume = 0; 
    }


    private IEnumerator FadeInMusic(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true; 
            audioSource.Play();
        }

        audioSource.volume = 0; 

        for (float t = 0; t <= fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, volumnMax, t / fadeTime);
            yield return null;
        }

        audioSource.volume = volumnMax; 
    }


}


