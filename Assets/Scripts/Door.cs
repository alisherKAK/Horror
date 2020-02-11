using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen, canBeOpened;
    private float doorOpenAngle = 90f;
    private float doorCloseAngle = 0f;
    private float smooth = 2f;

    private void Start()
    {
        canBeOpened = false;
        isOpen = false;
    }

    public void ChangeDoorState()
    {
        isOpen = !isOpen;
    }

    public void UseKey()
    {
        canBeOpened = true;
        isOpen = false;
    }

    private void Update()
    {
        if(isOpen && canBeOpened)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClose = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.parent.localRotation = Quaternion.Slerp(transform.parent.localRotation, targetRotationClose, smooth * Time.deltaTime);
        }
    }
}
