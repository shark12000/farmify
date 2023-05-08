using UnityEngine;
public class PositionChanged : MonoBehaviour
{
    public int transformX;
    public int transformY;

    public void OnButtonPressed(bool boolean)
    {
        Debug.Log(boolean);
        Vector3 transformPosition = transform.position;
        
        if (boolean)
        {
            Vector3 newPosition = new Vector3(transformX, transformY, transformPosition.z);
            transform.position = newPosition;
        }
        else
        {
            Vector3 resetPosition = Vector3.zero;
            transform.position = resetPosition;
        }
    }
}