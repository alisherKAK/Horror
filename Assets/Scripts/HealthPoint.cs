using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthPoint : MonoBehaviour
{
    [SerializeField]
    private float maxHealthPoint;

    [SerializeField]
    private Image healthIndecator;

    [SerializeField]
    private GameObject gameOverPanel;

    private float currentHealthPoint;

    private void Start()
    {
        currentHealthPoint = maxHealthPoint;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(currentHealthPoint > 0)
        {
            Vector3 healthSize = new Vector3(currentHealthPoint / maxHealthPoint,1,1);
            healthIndecator.transform.localScale = healthSize;
            return;
        }

        gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        gameOverPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Screamer")
        {
            Damage(10);
        }    
    }

    public void Heal(float health)
    {
        currentHealthPoint += health;
    }

    public void Heal()
    {
        currentHealthPoint = maxHealthPoint;
    }

    public void Damage(float damage)
    {
        currentHealthPoint -= damage;
    }
}
