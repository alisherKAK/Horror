using UnityEngine;

public class WoodBoxCode : MonoBehaviour
{
    [SerializeField]
    private string code;

    [SerializeField]
    private int codeLength;

    void Start()
    {
        for(int i = 0; i < codeLength; i++)
        {
            int number = Random.Range(0, 9);
            code += number.ToString();
        }
    }

    public string GetCode()
    {
        return code;
    }
}
