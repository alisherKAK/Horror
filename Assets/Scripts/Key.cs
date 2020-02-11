using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private Door door;

    public void PickUp()
    {
        door.UseKey();
        Destroy(gameObject);
    }
}
