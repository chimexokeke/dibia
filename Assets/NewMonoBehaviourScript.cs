using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from arrow keys (or WASD keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down arrows

        // Create a vector for movement direction
        Vector2 movement = new Vector2(moveX, moveY);

        // Move the sprite by changing its position
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
