using UnityEditor;
using UnityEngine;

public class ReverseSpriteSheet : EditorWindow
{
    [MenuItem("Tools/Reverse Sprite Sheet")]
    public static void OpenWindow()
    {
        GetWindow<ReverseSpriteSheet>("Reverse Sprite Sheet");
    }

    public Texture2D spriteSheet;
    public bool horizontalFlip;
    public bool verticalFlip;

    void OnGUI()
    {
        spriteSheet = EditorGUILayout.ObjectField("Sprite Sheet", spriteSheet, typeof(Texture2D), false) as Texture2D;
        horizontalFlip = EditorGUILayout.Toggle("Horizontal Flip", horizontalFlip);
        verticalFlip = EditorGUILayout.Toggle("Vertical Flip", verticalFlip);
        

        if (GUILayout.Button("Reverse"))
        {
            Reverse(spriteSheet, horizontalFlip, verticalFlip);
        }
    }

    private void Reverse(Texture2D spriteSheet, bool horizontalFlip, bool verticalFlip)
    {
        Color[] originalPixels = spriteSheet.GetPixels();
        int width = spriteSheet.width;
        int height = spriteSheet.height;

        Color[] newPixels = new Color[originalPixels.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                newPixels[index] = originalPixels[index]; // Preserve original color
            }
        }

        if (horizontalFlip)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width / 2; x++)
                {
                    int index = y * width + x;
                    int flippedIndex = y * width + (width - x - 1);

                    Color temp = newPixels[index];
                    newPixels[index] = newPixels[flippedIndex];
                    newPixels[flippedIndex] = temp;
                }
            }
        }

        if (verticalFlip)
        {
            for (int y = 0; y < height / 2; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    int flippedIndex = (height - y - 1) * width + x;

                    Color temp = newPixels[index];
                    newPixels[index] = newPixels[flippedIndex];
                    newPixels[flippedIndex] = temp;
                }
            }
        }

        spriteSheet.SetPixels(newPixels);
        spriteSheet.Apply(true);

        Debug.Log("Sprite sheet reversed successfully!");
    }
}