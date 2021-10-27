using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable] public class ColorEvent : UnityEvent<Color> {}

public class ColorPicker : MonoBehaviour
{
    public ColorEvent OnColorSelect;
    RectTransform rect;
    Texture2D colorTexture;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    void Update()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out delta);

        float width = rect.rect.width;
        float height = rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);

        float x = Mathf.Clamp(delta.x / width, 0f, 1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);

        int texX = Mathf.RoundToInt(x * colorTexture.width);
        int texY = Mathf.RoundToInt(y * colorTexture.height);

        Color color = colorTexture.GetPixel(texX, texY);

        if(!(texX == 0 || texX == 800 || texY == 0 || texY == 800 || color == Color.black)) {
            if(Input.GetMouseButtonDown(0)) {
                OnColorSelect?.Invoke(color);
                Debug.Log("TexX: " + texX.ToString() + "    TexY: " + texY.ToString());
                ChangeByColor.instance.ColorSelected(color);
            }
        }
        
    }
}
