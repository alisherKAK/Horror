using UnityEngine;

public class NearScreamer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioNearScreamer;

    public void PlayAudio()
    {
        audioNearScreamer.Play();
    }

    public void StopAudio()
    {
        audioNearScreamer.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioNearScreamer.Play();    
    }

    private void OnTriggerExit(Collider other) 
    {
        audioNearScreamer.Stop();    
    }
}
