using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private Sprite _onSprite;

    public void ChangeSprite(Button button)
    {
        if (button.GetComponent<Image>().sprite == _offSprite)
            button.GetComponent<Image>().sprite = _onSprite;
        else if (button.GetComponent<Image>().sprite == _onSprite)
            button.GetComponent<Image>().sprite = _offSprite;
    }
}
