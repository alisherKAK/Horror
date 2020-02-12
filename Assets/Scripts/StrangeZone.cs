using UnityEngine;

public class StrangeZone : MonoBehaviour
{
    [SerializeField]
    private Light flashLight;

    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "StrangeZone")
        {
            gameObject.GetComponent<FlashLight>().enabled = false;
            flashLight.enabled = false;
            audio.Play();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "StrangeZone")
        {
            gameObject.GetComponent<FlashLight>().enabled = true;
            audio.Stop();
        }
    }
}
