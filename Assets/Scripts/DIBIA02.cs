using System.Collections;
using CustomYields;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DIBIA02 : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;  // Anim00 to Anim15 VideoPlayers
    public RawImage[] rawImages;        // Vid00 to Vid15 RawImages for each video
    public TextMeshProUGUI promptText;  // TextMeshPro for displaying instructions
    public Slider timeSlider;           // TimeSlider for countdown display

    private string currentCorrectInput = "";
    private float timeRemaining = 0;
    private bool isTimerOn = false;
    private bool isInGame = true;
    private bool isEndingGame = false;

    private Coroutine gameCoroutine;

    void Update()
    {
        if (!isInGame)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5)) RetryGame();
            if (!isEndingGame && Input.GetKeyDown(KeyCode.Alpha0)) EndGame();
            return;
        }
        
        if (isTimerOn) TimerUpdate();
    }

    private void RetryGame()
    {
        HidePrompt();
        gameCoroutine = StartCoroutine(RunGame());
    }

    private void TimerUpdate()
    {
        // Countdown logic for user input with time slider
        
        timeRemaining -= Time.deltaTime;
        timeSlider.value = timeRemaining;
    }

    VideoPlayer PlayLoopingVideo(int index)
    {
        StopAllVideos();

        VideoPlayer player = videoPlayers[index];

        Debug.Log($"PLAYING VIDEO {index}", player.gameObject);

        player.isLooping = true;
        player.Play();
        rawImages[index].enabled = true;

        return player;
    }

    VideoPlayer PlayVideo(int index)
    {
        StopAllVideos();
        var player = videoPlayers[index];
        player.isLooping = false;
        player.Play();
        
        Debug.Log($"PLAYING VIDEO {index}", player.gameObject);

        rawImages[index].enabled = true;

        return player;
    }

    void StopAllVideos()
    {
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Stop();
        }
        foreach (RawImage image in rawImages)
        {
            image.enabled = false;
        }
    }

    void ShowPrompt(string text)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = text;
    }

    void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }

    void ShowTimeSlider(float duration)
    {
        isTimerOn = true;
        timeRemaining = duration;
        timeSlider.gameObject.SetActive(true);
        timeSlider.maxValue = duration;
        timeSlider.value = duration;
    }

    void HideTimeSlider()
    {
        timeSlider.gameObject.SetActive(false);
    }
    
    

    IEnumerator Start()
    {
        // Disable all videos and raw images initially
        foreach (VideoPlayer player in videoPlayers)
        {
            player.Stop();
        }
        foreach (RawImage image in rawImages)
        {
            image.enabled = false;
        }

        timeSlider.gameObject.SetActive(false);  // Hide timeslider initially
        
        // Start the game loop with Anim00
        PlayLoopingVideo(0);
        ShowPrompt("PRESS 0 FOR SOME DISQUIETNESS!!!");

        yield return new WaitForPhoneNumber("0");
        
        HidePrompt();

        yield return WaitForVideo(PlayVideo(1));

        gameCoroutine = StartCoroutine(RunGame());
    }

    IEnumerator RunGame()
    {
        isInGame = true;
        VideoPlayer currentPlayer;

        PlayLoopingVideo(2); // Play Anim02 in a loop
        ShowPrompt("CALL CHINEDU: 08132223688");
        ShowTimeSlider(15f);
        var wait = new WaitForPhoneNumber("08132223688", 15f);
        yield return wait;
        if (!wait.Success)
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();
        
        currentPlayer = PlayVideo(3); // Play Anim03
        yield return WaitForVideo(currentPlayer);
        
        currentPlayer = PlayVideo(4); // Play Anim04
        yield return WaitForVideo(currentPlayer);

        PlayLoopingVideo(5);
        ShowPrompt("PRESS 5 TO SPEAK TO CHINEDU");
        ShowTimeSlider(10f);
        yield return new WaitForPhoneNumber("5", 10f);
        if (!wait.Success) 
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();

        currentPlayer = PlayVideo(6); // Play Anim06
        yield return WaitForVideo(currentPlayer);

        PlayLoopingVideo(7);
        ShowPrompt("PRESS 19 TO PLAY WITH HIS FINGERS");
        ShowTimeSlider(7f);
        wait = new WaitForPhoneNumber("19", 7f);
        yield return wait;
        if (!wait.Success) 
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();

        currentPlayer = PlayVideo(8); // Play Anim08
        yield return WaitForVideo(currentPlayer);

        PlayLoopingVideo(9);
        ShowPrompt("PRESS 375 TO PLAY WITH HIS OTHER FINGERS");
        ShowTimeSlider(5f);
        wait = new WaitForPhoneNumber("375", 5f);
        yield return wait;
        if (!wait.Success) 
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();

        currentPlayer = PlayVideo(10); // Play Anim08
        yield return WaitForVideo(currentPlayer);

        PlayLoopingVideo(11);
        ShowPrompt("PRESS 3495 TO MASSAGE HIS FOOT");
        ShowTimeSlider(3f);
        wait = new WaitForPhoneNumber("3495", 3f);
        yield return wait;
        if (!wait.Success) 
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();

        currentPlayer = PlayVideo(12); // Play Anim08
        yield return WaitForVideo(currentPlayer);

        PlayLoopingVideo(13);
        ShowPrompt("PRESS 0000000 TO WRAP IT UP");
        ShowTimeSlider(3f);
        wait = new WaitForPhoneNumber("0000000", 3f);
        yield return wait;
        if (!wait.Success) 
        {
            HandleTimeOut();
            yield break;
        }
        HidePrompt();
        HideTimeSlider();
        
        currentPlayer = PlayVideo(14); // Play Anim14
        ShowPrompt("IT IS DONE!! THANKS FOR PLAYING!!!");
        yield return WaitForVideo(currentPlayer);
    }

    private WaitForSeconds WaitForVideo(VideoPlayer player)
    {
        return new WaitForSeconds((float) player.clip.length);
    }

    void HandleTimeOut()
    {
        Debug.Log("TIME OUT");
        isTimerOn = false;
        isInGame = false;
        
        StopCoroutine(gameCoroutine);
        StopAllVideos();
        
        HideTimeSlider();
        ShowPrompt("Retry? Press 5 to retry or 0 to quit.");
    }

    void EndGame()
    {
        isEndingGame = true;
        PlayVideo(15); // Play Anim15
        ShowPrompt("CHINEDU ESCAPED! GAME OVER!");
        StartCoroutine(QuitGameAfterDelay());
    }

    System.Collections.IEnumerator QuitGameAfterDelay()
    {
        yield return new WaitForSeconds(2);
        Application.Quit(); // End game
    }
}
