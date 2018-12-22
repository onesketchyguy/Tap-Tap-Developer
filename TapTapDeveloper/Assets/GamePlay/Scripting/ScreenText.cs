using UnityEngine;
using UnityEngine.UI;

public class ScreenText : MonoBehaviour
{
    private Text myText;

    public bool isPublic;

    public int textIndex;

    private void Start()
    {
        myText = GetComponent<Text>();
    }

    void Update()
    {
        myText.text = (isPublic)? ((PublicText.Text[textIndex] != null)? PublicText.Text[textIndex] : ""): PlayerText.Text;
    }
}
