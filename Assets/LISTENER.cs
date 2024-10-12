using UnityEngine;

public class LISTENER : MonoBehaviour
{
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
}
