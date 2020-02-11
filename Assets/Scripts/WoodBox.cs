using UnityEngine;

public class WoodBox : MonoBehaviour
{
    private bool isOpen = false;
    private float woodBoxOpenAngle = -57f;
    private float smooth = 2f;

    public void Open()
    {
        isOpen = true;
    }

    private void Update()
    {
        if(isOpen)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(woodBoxOpenAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
    }
}
