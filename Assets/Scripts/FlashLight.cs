using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Light _flashLight;
    private bool isOn = true;

    [SerializeField]
    private float _batteryLife, _lightDrain;

    private float _currentBatteryLife;

    [SerializeField]
    private Image _batteryIdicator;

    // Start is called before the first frame update
    private void Start()
    {
        _currentBatteryLife = _batteryLife;
    }

    // Update is called once per frame
    private void Update()
    {
        _flashLight.intensity = _currentBatteryLife;

        if(Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            _flashLight.enabled = isOn;
        }

        if(isOn)
        {
            if(_currentBatteryLife > 0)
            {
                _currentBatteryLife -= _lightDrain * Time.deltaTime;
                Debug.Log("Current Battery: " + _currentBatteryLife);
                Vector3 batterySize = new Vector3(_currentBatteryLife / _batteryLife, _batteryIdicator.transform.localScale.y, _batteryIdicator.transform.localScale.z);
                _batteryIdicator.transform.localScale = batterySize;
            }
            else
            {
                _currentBatteryLife = 0;
                isOn = false;
                _flashLight.enabled = false;
            }
        }
    }
}
