using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorHelper : MonoBehaviour
{
    [MenuItem("EditorHelper/SliceSprites")]
    static void SliceSprites()
    {
        int sliceWidth = 32;
        int sliceHeight = 32;

        string folderPath = "ToSlice";

        Object[] spriteSheets = Resources.LoadAll(folderPath, typeof(Texture2D));
        // Debug.Log("spriteSheets.Length: " + spriteSheets.Length);

        for (int z = 0; z < spriteSheets.Length; z++)
        {
            // Debug.Log("z: " + z + " spriteSheets[z]: " + spriteSheets[z]);

            string path = AssetDatabase.GetAssetPath(spriteSheets[z]);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;
            ti.spriteImportMode = SpriteImportMode.Multiple;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();

            Texture2D spriteSheet = spriteSheets[z] as Texture2D;

            for (int i = 0; i < spriteSheet.width; i += sliceWidth)
            {
                for (int j = spriteSheet.height; j > 0; j -= sliceHeight)
                {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(0.5f, 0.5f);
                    smd.alignment = (int)SpriteAlignment.Center;
                    smd.name = (spriteSheet.height - j) / sliceHeight + ", " + i / sliceWidth;
                    smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);

                    newData.Add(smd);
                }
            }

            SetSpriteMetaData(ti, newData);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        // Debug.Log("Done Slicing!");
    }

    static void SetSpriteMetaData(TextureImporter textureImporter, List<SpriteMetaData> spriteMetaDataList)
    {
        // Use SerializedObject to manipulate the serialized properties
        SerializedObject so = new SerializedObject(textureImporter);
        SerializedProperty spritesheetProp = so.FindProperty("m_SpriteSheet.m_Sprites");

        // Clear existing spritesheet data
        spritesheetProp.ClearArray();

        // Add new spritesheet data
        for (int i = 0; i < spriteMetaDataList.Count; i++)
        {
            spritesheetProp.InsertArrayElementAtIndex(i);
            SerializedProperty spriteProp = spritesheetProp.GetArrayElementAtIndex(i);
            spriteProp.FindPropertyRelative("m_Rect").rectValue = spriteMetaDataList[i].rect;
            spriteProp.FindPropertyRelative("m_Name").stringValue = spriteMetaDataList[i].name;
            spriteProp.FindPropertyRelative("m_Alignment").intValue = spriteMetaDataList[i].alignment;
            spriteProp.FindPropertyRelative("m_Pivot").vector2Value = spriteMetaDataList[i].pivot;
        }

        // Apply modifications
        so.ApplyModifiedProperties();
    }
}