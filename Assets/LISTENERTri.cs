using UnityEngine;

public class LISTENERTri : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
       Debug.Log(msg);
        if(msg == "2")
        {
            MoveUp();
        }
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {

    }
    private void MoveUp()
    {

    }
    private void Move()
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
