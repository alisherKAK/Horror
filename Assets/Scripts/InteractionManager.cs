using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InteractionManager : MonoBehaviour{

    [SerializeField]
    private Image handImage;

    [SerializeField]
    private float interactDistance;

    [SerializeField]
    private GameObject paperPanel, woodBoxPanel, woodBox;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private FlashLight flashLight;

    [SerializeField]
    private Button firstDigitAddButton, secondDigitAddButton, thirdDigitAddButton, fourthDigitAddButton;

    [SerializeField]
    private Button firstDigitMinusButton, secondDigitMinusButton, thirdDigitMinusButton, fourthDigitMinusButton;

    [SerializeField]
    private Text firstDigit, secondDigit, thirdDigit, fourthDigit;

    [SerializeField]
    private WoodBoxCode woodBoxCode;

    [SerializeField]
    private Light candleLight;

    [SerializeField]
    private Door doorPaper;

    private bool isReading;
    private int readPaper;

    private void Start(){
        if(candleLight != null)
        {
            candleLight.enabled = false;
        }

        if(handImage != null)
        {
        // отключаем руку
            handImage.gameObject.SetActive(false);
        }
        
        if(paperPanel != null)
        {
            paperPanel.SetActive(false);
        }
        if(woodBox != null)
        {
            woodBoxPanel.SetActive(false);
        }

        isReading = false;
        readPaper = 0;
        
        firstDigitAddButton.onClick.AddListener(() => AddDigit(1));
        secondDigitAddButton.onClick.AddListener(() => AddDigit(2));
        thirdDigitAddButton.onClick.AddListener(() => AddDigit(3));
        fourthDigitAddButton.onClick.AddListener(() => AddDigit(4));

        firstDigitMinusButton.onClick.AddListener(() => MinusDigit(1));
        secondDigitMinusButton.onClick.AddListener(() => MinusDigit(2));
        thirdDigitMinusButton.onClick.AddListener(() => MinusDigit(3));
        fourthDigitMinusButton.onClick.AddListener(() => MinusDigit(4));
    }

    private void AddDigit(int id)
    {
        switch(id)
        {
            case 1:
                if(int.Parse(firstDigit.text) == 9)
                {
                    firstDigit.text = "0";
                    break;
                }
                firstDigit.text = (int.Parse(firstDigit.text) + 1).ToString();
                break;
            case 2:
                if(int.Parse(secondDigit.text) == 9)
                {
                    secondDigit.text = "0";
                    break;
                }
                secondDigit.text = (int.Parse(secondDigit.text) + 1).ToString();
                break;
            case 3:
                if(int.Parse(thirdDigit.text) == 9)
                {
                    thirdDigit.text = "0";
                    break;
                }
                thirdDigit.text = (int.Parse(thirdDigit.text) + 1).ToString();
                break;
            case 4:
                if(int.Parse(fourthDigit.text) == 9)
                {
                    fourthDigit.text = "0";
                    break;
                }
                fourthDigit.text = (int.Parse(fourthDigit.text) + 1).ToString();
                break;
        }

        string code = firstDigit.text + secondDigit.text + thirdDigit.text + fourthDigit.text;
        CheckCode(code);
    }

    private void MinusDigit(int id)
    {
        switch(id)
        {
            case 1:
                if(int.Parse(firstDigit.text) == 0)
                {
                    firstDigit.text = "9";
                    break;
                }
                firstDigit.text = (int.Parse(firstDigit.text) - 1).ToString();
                break;
            case 2:
                if(int.Parse(secondDigit.text) == 0)
                {
                    secondDigit.text = "9";
                    break;
                }
                secondDigit.text = (int.Parse(secondDigit.text) - 1).ToString();
                break;
            case 3:
                if(int.Parse(thirdDigit.text) == 0)
                {
                    thirdDigit.text = "9";
                    break;
                }
                thirdDigit.text = (int.Parse(thirdDigit.text) - 1).ToString();
                break;
            case 4:
                if(int.Parse(fourthDigit.text) == 0)
                {
                    fourthDigit.text = "9";
                    break;
                }
                fourthDigit.text = (int.Parse(fourthDigit.text) - 1).ToString();
                break;
        }

        string code = firstDigit.text + secondDigit.text + thirdDigit.text + fourthDigit.text;
        CheckCode(code);
    }

    private void CheckCode(string code)
    {
        if(code == woodBoxCode.GetCode())
        {
            woodBox.GetComponentInChildren<WoodBox>().Open();
            woodBox.GetComponent<BoxCollider>().enabled = false;
            woodBoxPanel.SetActive(false);
            gameObject.GetComponentsInParent<RigidbodyFirstPersonController>()[0].enabled = true;
        }
    }

    private void Update(){
        Ray ray = new Ray (transform.position, transform.forward);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray, out raycastHit, interactDistance, layerMask)){
            // если рука не отображается
            if(!handImage.gameObject.activeSelf){
                // показать картинку
                handImage.gameObject.SetActive(true);
            }

            // если нажата клавиша Е
            if(Input.GetKeyDown(KeyCode.E)){
                // если смотрю на батарейки
                if(raycastHit.transform.tag == "Battery"){
                    // пополнить заряд фонарика
                    flashLight.AddEnergy();
                    // уничтожить батарейки
                    Destroy(raycastHit.transform.gameObject);
                }
                else if(raycastHit.transform.tag == "Paper")
                {
                    paperPanel.SetActive(true);
                    isReading = true;
                    gameObject.GetComponentsInParent<RigidbodyFirstPersonController>()[0].enabled = false;

                    string text = raycastHit.transform.gameObject.GetComponent<Paper>().GetText();

                    paperPanel.GetComponent<PaperPanel>().SetText(text);

                    readPaper++;

                    if(readPaper == 3)
                    {
                        doorPaper.UseKey();
                    }
                }
                else if(raycastHit.transform.tag == "Key")
                {
                    Key key = raycastHit.transform.gameObject.GetComponent<Key>();
                    if(key != null)
                    {
                        key.PickUp();
                    }
                }
                else if(raycastHit.transform.tag == "Door")
                {
                    Door door = raycastHit.transform.gameObject.GetComponent<Door>();
                    if(door != null)
                    {
                        door.ChangeDoorState();
                    }
                }
                else if(raycastHit.transform.tag == "WoodCodeBox")
                {
                    gameObject.GetComponentsInParent<RigidbodyFirstPersonController>()[0].enabled = false;
                    woodBoxPanel.SetActive(true);
                    woodBox = raycastHit.transform.gameObject;
                }
                else if(raycastHit.transform.tag == "Candle")
                {
                    candleLight.enabled = !candleLight.enabled;
                }
                else if(raycastHit.transform.tag == "Pill")
                {
                    gameObject.GetComponentInParent<HealthPoint>().Heal();
                    Destroy(raycastHit.transform.gameObject);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(raycastHit.transform.tag == "Paper" && isReading)
                {
                    gameObject.GetComponentsInParent<RigidbodyFirstPersonController>()[0].enabled = true;
                    isReading = false;
                    paperPanel.SetActive(false);
                }
                else if(raycastHit.transform.tag == "WoodCodeBox")
                {
                    woodBoxPanel.SetActive(false);
                    gameObject.GetComponentsInParent<RigidbodyFirstPersonController>()[0].enabled = true;
                }
            }
        }else{
            //выключаем картинку
            handImage.gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        firstDigitAddButton.onClick.RemoveAllListeners();
        firstDigitMinusButton.onClick.RemoveAllListeners();

        secondDigitAddButton.onClick.RemoveAllListeners();
        secondDigitMinusButton.onClick.RemoveAllListeners();

        thirdDigitAddButton.onClick.RemoveAllListeners();
        thirdDigitMinusButton.onClick.RemoveAllListeners();

        fourthDigitAddButton.onClick.RemoveAllListeners();
        fourthDigitMinusButton.onClick.RemoveAllListeners();
    }
}
