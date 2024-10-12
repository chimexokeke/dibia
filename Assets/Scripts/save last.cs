using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DIBIA : MonoBehaviour
{
    [Header("Video Players")]
    public VideoPlayer Anim01;
    public VideoPlayer Anim02;
    public VideoPlayer Anim03;
    public VideoPlayer Anim04;
    public VideoPlayer Anim05;
    public VideoPlayer Anim06;
    public VideoPlayer Anim07;
    public VideoPlayer Anim08;
    public VideoPlayer Anim09;
    public VideoPlayer Anim10;
    public VideoPlayer Anim11;
    public VideoPlayer Anim12;
    public VideoPlayer Anim13;
    public VideoPlayer Anim14;
    public VideoPlayer Anim15;

    [Header("Raw Images")]
    public RawImage vid01;
    public RawImage vid02;
    public RawImage vid03;
    public RawImage vid04;
    public RawImage vid05;
    public RawImage vid06;
    public RawImage vid07;
    public RawImage vid08;
    public RawImage vid09;
    public RawImage vid10;
    public RawImage vid11;
    public RawImage vid12;
    public RawImage vid13;
    public RawImage vid14;
    public RawImage vid15;

    [Header("UI Elements")]
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI promptText;

    private bool gameStarted = false;
    private bool waitingForRetryDecision = false;
    private string playerInput = "";
    private float timer = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the initial text with bold formatting
        promptText.text = "<b>PRESS 5 FOR SOME DISQUIETNESS!!!</b>";
        promptText.fontSize = 36; // Adjust this value as needed
        promptText.alignment = TextAlignmentOptions.Center;

        // Turn off all videos and raw images
        DisableAllVideosAndImages();
    }

    // Update the existing Update method
    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartGame();
        }

        if (waitingForRetryDecision)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                StartCoroutine(RetrySequence());
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0)) // Changed from Asterisk to Alpha0
            {
                StartCoroutine(EndGameSequence());
            }
        }

        // ... any existing code in Update ...
    }

    void StartGame()
    {
        gameStarted = true;
        promptText.text = ""; // Clear the initial prompt
        StartCoroutine(GameSequence());
    }

    IEnumerator GameSequence()
    {
        yield return PlayVideo(Anim01, vid01);
        StartCoroutine(PlayVideo2Loop());
    }

    IEnumerator PlayVideo(VideoPlayer video, RawImage image)
    {
        DisableAllVideosAndImages();
        video.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        video.Play();
        yield return new WaitForSeconds((float)video.clip.length);
    }

    IEnumerator PlayVideo2Loop()
    {
        DisableAllVideosAndImages();
        Anim02.gameObject.SetActive(true);
        vid02.gameObject.SetActive(true);
        Anim02.isLooping = true;
        Anim02.Play();

        promptText.text = "CALL CHINEDU: 08132223688";
        slider.gameObject.SetActive(true);
        slider.maxValue = 15f;
        timer = 15f;
        playerInput = "";

        while (timer > 0 && playerInput != "08132223688")
        {
            timer -= Time.deltaTime;
            slider.value = timer;

            foreach (char c in Input.inputString)
            {
                if (char.IsDigit(c))
                {
                    playerInput += c;
                    promptText.text = playerInput;
                }
            }

            yield return null;
        }

        Anim02.Stop();

        if (playerInput == "08132223688")
        {
            yield return PlayVideo(Anim03, vid03);
            // Continue with the next part of the game
        }
        else
        {
            promptText.text = "Time's up! Press 5 to retry or 0 to quit."; // Updated text
            waitingForRetryDecision = true;
            while (waitingForRetryDecision)
            {
                yield return null;
            }
        }
    }

    IEnumerator RetrySequence()
    {
        waitingForRetryDecision = false;
        promptText.text = "";
        playerInput = "";
        // Wait for a frame to ensure the '5' key press is not captured
        yield return null;
        StartCoroutine(PlayVideo2Loop());
    }

    IEnumerator EndGameSequence()
    {
        waitingForRetryDecision = false;
        DisableAllVideosAndImages();

        // Play Video 1 in reverse
        Anim01.gameObject.SetActive(true);
        vid01.gameObject.SetActive(true);
        Anim01.playbackSpeed = -1;
        Anim01.time = (float)Anim01.clip.length;
        Anim01.Play();

        // Show "GAME OVER" text
        promptText.text = "GAME OVER";
        promptText.fontSize = 48; // Adjust size as needed
        promptText.color = Color.red; // Set color to red

        // Wait for the video to finish playing in reverse
        yield return new WaitForSeconds((float)Anim01.clip.length);

        // Stop the video and quit the application
        Anim01.Stop();
        yield return new WaitForSeconds(2); // Wait for 2 seconds to show the "GAME OVER" text
        Application.Quit();
    }

    void HandlePlayerInput()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                playerInput += c;
                promptText.text = playerInput;
            }
        }
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        slider.value = timer;
    }

    void DisableAllVideosAndImages()
    {
        // Disable all video players
        Anim01.gameObject.SetActive(false);
        Anim02.gameObject.SetActive(false);
        Anim03.gameObject.SetActive(false);
        Anim04.gameObject.SetActive(false);
        Anim05.gameObject.SetActive(false);
        Anim06.gameObject.SetActive(false);
        Anim07.gameObject.SetActive(false);
        Anim08.gameObject.SetActive(false);
        Anim09.gameObject.SetActive(false);
        Anim10.gameObject.SetActive(false);
        Anim11.gameObject.SetActive(false);
        Anim12.gameObject.SetActive(false);
        Anim13.gameObject.SetActive(false);
        Anim14.gameObject.SetActive(false);
        Anim15.gameObject.SetActive(false);

        // Disable all raw images
        vid01.gameObject.SetActive(false);
        vid02.gameObject.SetActive(false);
        vid03.gameObject.SetActive(false);
        vid04.gameObject.SetActive(false);
        vid05.gameObject.SetActive(false);
        vid06.gameObject.SetActive(false);
        vid07.gameObject.SetActive(false);
        vid08.gameObject.SetActive(false);
        vid09.gameObject.SetActive(false);
        vid10.gameObject.SetActive(false);
        vid11.gameObject.SetActive(false);
        vid12.gameObject.SetActive(false);
        vid13.gameObject.SetActive(false);
        vid14.gameObject.SetActive(false);
        vid15.gameObject.SetActive(false);

        slider.gameObject.SetActive(false);
        promptText.text = "";
        promptText.color = Color.white; // Reset text color
        promptText.fontSize = 36; // Reset font size
    }
}