using System;
using System.Collections;
using UnityEngine;
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
        Medium = 1,
        High = 2
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
    void Awake()
    { 
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if(scene.name == "MainMenu")
        {
            MainMenuMusic.Play();
        }
    }
    
    void Update()
    {
        if (AudioSettings.dspTime >= songEndTime || !GameMusic[0].isPlaying)
        {
            StartGameMusic();
            timeSinceMusicStarted = 0;
            SetMusicIntensity(currentIntensity);
        }
        timeSinceGameStarted += Time.deltaTime;
        
    }
    
    public void StartGameMusic()
    {
        MainMenuMusic.Stop();
        Double startTime = AudioSettings.dspTime + 2d;
        timeSinceMusicStarted = 0;
        GameMusic[0].PlayScheduled(startTime + 2d);
        GameMusic[0].volume = volumnMax;
        GameMusic[1].PlayScheduled(startTime + 2d);
        GameMusic[1].volume = 0;
        GameMusic[2].PlayScheduled(startTime + 2d);
        GameMusic[2].volume = 0;
        songEndTime = startTime + (60d / gameMusicBpm) * GameMusic[0].clip.samples -4d;
    }
    public void SetMusicIntensity(MusicIntensity intensity)
    {
        if(currentIntensity == intensity)
        {
            return;
        }
        //Debug.Log("GameMusic0:"+GameMusic[0].isPlaying + " " + GameMusic[0].volume);
        //Debug.Log("GameMusic1:"+GameMusic[1].isPlaying + " " + GameMusic[1].volume);
        //Debug.Log("GameMusic2:"+GameMusic[2].isPlaying + " " + GameMusic[2].volume);
        switch (intensity)
        {
            case MusicIntensity.Low:
                StartCoroutine(FadeOutMusic(GameMusic[1]));
                StartCoroutine(FadeOutMusic(GameMusic[2]));
                StartCoroutine(FadeInMusic(GameMusic[0]));
                break;
            case MusicIntensity.Medium:
                StartCoroutine(FadeOutMusic(GameMusic[0]));
                StartCoroutine(FadeOutMusic(GameMusic[2]));
                StartCoroutine(FadeInMusic(GameMusic[1]));
                break;
            case MusicIntensity.High:
                StartCoroutine(FadeOutMusic(GameMusic[0]));
                StartCoroutine(FadeOutMusic(GameMusic[1]));
                StartCoroutine(FadeInMusic(GameMusic[2]));
                break;
        }
    }

    private IEnumerator FadeOutMusic(AudioSource audioSource)
    { 
        float initialVolume = audioSource.volume;
        for (float t = 0; t <= fadeTime; t += Time.deltaTime) 
        {
            float volume = Mathf.Lerp(initialVolume, 0, t / fadeTime);
            audioSource.volume = volume;
            yield return null;
        }
    
        audioSource.volume = 0;
    }
    
    private IEnumerator FadeInMusic(AudioSource audioSource)
    {
        float initialVolume = audioSource.volume;
    
        for (float t = 0; t <= fadeTime; t += Time.deltaTime) 
        {
            float volume = Mathf.Lerp(initialVolume, volumnMax, t / fadeTime);
            audioSource.volume = volume;
            yield return null;
        }
    
        audioSource.volume = volumnMax;
    }
}
