using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private GameObject _batterySpawnPoint1, _batterySpawnPoint2, _batterySpawnPoint3; 

    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = Random.Range(5, 10);
    }
    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0)
        {
            int spawnPoint = Random.Range(1, 4);

            Vector3 position;
            Quaternion rotation;

            switch(spawnPoint)
            {
                case 1:
                    position = new Vector3(_batterySpawnPoint1.transform.position.x, _batterySpawnPoint1.transform.position.y, _batterySpawnPoint1.transform.position.z);
                    rotation = new Quaternion(_batterySpawnPoint1.transform.rotation.x, _batterySpawnPoint1.transform.rotation.y, _batterySpawnPoint1.transform.rotation.z, _batterySpawnPoint1.transform.rotation.w);
                    Instantiate(gameObject, position, rotation);
                    break;
                case 2:
                    position = new Vector3(_batterySpawnPoint2.transform.position.x, _batterySpawnPoint2.transform.position.y, _batterySpawnPoint2.transform.position.z);
                    rotation = new Quaternion(_batterySpawnPoint2.transform.rotation.x, _batterySpawnPoint2.transform.rotation.y, _batterySpawnPoint2.transform.rotation.z, _batterySpawnPoint2.transform.rotation.w);
                    Instantiate(gameObject, position, rotation);
                    break;
                case 3:
                    position = new Vector3(_batterySpawnPoint3.transform.position.x, _batterySpawnPoint3.transform.position.y, _batterySpawnPoint3.transform.position.z);
                    rotation = new Quaternion(_batterySpawnPoint3.transform.rotation.x, _batterySpawnPoint3.transform.rotation.y, _batterySpawnPoint3.transform.rotation.z, _batterySpawnPoint3.transform.rotation.w);
                    Instantiate(gameObject, position, rotation);
                    break;
            }
            Debug.Log("Created");

            _spawnTimer = Random.Range(100, 120);
        }
        
        Debug.Log("Spawn Timer: " + _spawnTimer);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Touched");
        if(other.tag == "Player")    
        {
            other.GetComponentInChildren<FlashLight>().AddEnergy();
            Destroy(gameObject);
        }
    }
}
