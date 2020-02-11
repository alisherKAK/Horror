using UnityEngine;
using UnityEngine.UI;

public class PaperPanel : MonoBehaviour
{
    [SerializeField]
    private Text paperText;

    public void SetText(string text)
    {
        paperText.text = text;
    }
}
