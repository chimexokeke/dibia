using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class DIBIA02 : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;  // Anim00 to Anim15 VideoPlayers
    public RawImage[] rawImages;        // Vid00 to Vid15 RawImages for each video
    public TextMeshProUGUI promptText;  // TextMeshPro for displaying instructions
    public Slider timeSlider;           // TimeSlider for countdown display

    private bool isWaitingForInput = false;
    private string currentCorrectInput = "";
    private float timeRemaining = 0;
    private bool gameEnded = false;

    void Start()
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
    }

    void Update()
    {
        if (gameEnded) return;

        // Check for key presses
        if (Input.GetKeyDown(KeyCode.Alpha0) && !isWaitingForInput)
        {
            PlayVideo(1); // Play Anim01
            //ProceedToNextStep(2);
        }

        // Countdown logic for user input with time slider
        if (isWaitingForInput && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeSlider.value = timeRemaining;

            if (timeRemaining <= 0)
            {
                HandleInputFailure();
            }
        }

        // Handle input success if player inputs the correct value
        if (Input.anyKeyDown && isWaitingForInput)
        {
            if (Input.inputString == currentCorrectInput)
            {
                HandleInputSuccess();
            }
        }
    }

    void PlayLoopingVideo(int index)
    {
        VideoPlayer player = videoPlayers[index];

        player.isLooping = true;
        player.Play();
        rawImages[index].enabled = true;
    }

    void PlayVideo(int index)
    {
        StopAllVideos();
        videoPlayers[index].Play();
        rawImages[index].enabled = true;
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
        timeSlider.gameObject.SetActive(true);
        timeSlider.maxValue = duration;
        timeSlider.value = duration;
    }

    void HideTimeSlider()
    {
        timeSlider.gameObject.SetActive(false);
    }

    void ProceedToNextStep(int nextVideoIndex)
    {
        // Logic to handle different steps in the game
        switch (nextVideoIndex)
        {
            case 2:
                PlayLoopingVideo(2); // Play Anim02 in a loop
                ShowPrompt("CALL CHINEDU: 08132223688");
                StartWaitingForInput("08132223688", 15f, nextVideoIndex + 1);
                break;
            case 3:
                PlayVideo(3); // Play Anim03
                ProceedToNextStep(4);
                break;
            case 4:
                PlayVideo(4); // Play Anim04
                ProceedToNextStep(5);
                break;
            case 5:
                PlayLoopingVideo(5); // Play Anim05 in a loop
                ShowPrompt("PRESS 5 TO SPEAK TO CHINEDU");
                StartWaitingForInput("5", 10f, nextVideoIndex + 1);
                break;
            case 6:
                PlayVideo(6); // Play Anim06
                ProceedToNextStep(7);
                break;
            case 7:
                PlayLoopingVideo(7); // Play Anim07 in a loop
                ShowPrompt("PRESS 19 TO PLAY WITH HIS FINGERS");
                StartWaitingForInput("19", 7f, nextVideoIndex + 1);
                break;
            case 8:
                PlayVideo(8); // Play Anim08
                ProceedToNextStep(9);
                break;
            case 9:
                PlayLoopingVideo(9); // Play Anim09 in a loop
                ShowPrompt("PRESS 375 TO PLAY WITH HIS OTHER FINGERS");
                StartWaitingForInput("375", 5f, nextVideoIndex + 1);
                break;
            case 10:
                PlayVideo(10); // Play Anim10
                ProceedToNextStep(11);
                break;
            case 11:
                PlayLoopingVideo(11); // Play Anim11 in a loop
                ShowPrompt("PRESS 3495 TO MASSAGE HIS FOOT");
                StartWaitingForInput("3495", 3f, nextVideoIndex + 1);
                break;
            case 12:
                PlayVideo(12); // Play Anim12
                ProceedToNextStep(13);
                break;
            case 13:
                PlayLoopingVideo(13); // Play Anim13 in a loop
                ShowPrompt("PRESS 0000000 TO WRAP IT UP");
                StartWaitingForInput("0000000", 2f, nextVideoIndex + 1);
                break;
            case 14:
                PlayVideo(14); // Play Anim14
                ShowPrompt("IT IS DONE!! THANKS FOR PLAYING!!!");
                break;
        }
    }

    void StartWaitingForInput(string correctInput, float duration, int nextVideoIndex)
    {
        currentCorrectInput = correctInput;
        timeRemaining = duration;
        ShowTimeSlider(duration);
        isWaitingForInput = true;
        StartCoroutine(WaitForInput(nextVideoIndex));
    }

    System.Collections.IEnumerator WaitForInput(int nextVideoIndex)
    {
        yield return new WaitForSeconds(timeRemaining);
        if (!isWaitingForInput) yield break;
        HandleInputFailure();
    }

    void HandleInputSuccess()
    {
        isWaitingForInput = false;
        HideTimeSlider();
        HidePrompt();
        ProceedToNextStep(3); // Continue to the next step
    }

    void HandleInputFailure()
    {
        isWaitingForInput = false;
        HideTimeSlider();
        ShowPrompt("Retry? Press 5 to retry or 0 to quit.");
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ProceedToNextStep(2); // Retry current step
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        PlayVideo(15); // Play Anim15
        ShowPrompt("CHINEDU ESCAPED! GAME OVER!");
        StartCoroutine(QuitGameAfterDelay());
    }

    System.Collections.IEnumerator QuitGameAfterDelay()
    {
        yield return new WaitForSeconds(10);
        Application.Quit(); // End game
    }
}
