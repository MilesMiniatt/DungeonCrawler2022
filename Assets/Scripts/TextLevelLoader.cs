using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 Class that will load in levels from the folders.
This is a singleton.
 */
public class TextLevelLoader : MonoBehaviour
{
    float currentBlockX, currentBlockY, currentBlockZ = 0;

    [SerializeField]
    GameObject map;

    public static TextLevelLoader instance;

    [SerializeField]
    List<Block> prefabs = new List<Block>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //For text upon sort. Ideally, the GameManager should call the ReadFromFile function and load a level depending on the player's progress
        //ReadFromFile("Level 1");
    }

    /*
     Function that reads all level files from a folder and generates a level.
    X-Z grid starts at bottom left with Z axis being vertical and X-axis being the horizontal axis.
    Must provide a valid level path or will result in an error.
     */
    public void LoadLevel(string Level)
    {
        string readFromFilePath = Application.streamingAssetsPath + "/Levels/" + Level;
        var levels = new DirectoryInfo(readFromFilePath);
        var levelsInfo = levels.GetFiles();
        GameObject map = new GameObject();

        currentBlockY = 0;

        foreach (FileInfo levelFile in levelsInfo)
        {
            string fileExtension = levelFile.Extension;

            //file reader will read meta files as well. We only need the text files so we apply this check
            if (fileExtension == ".txt")
            {
                Debug.Log("Loading file " + levelFile.Name);
                //YSlice object to keep editor clean. We parent each Y Slice to the map object
                GameObject YSlice = new GameObject();
                YSlice.transform.name = "YSlice " + currentBlockY;
                YSlice.transform.parent = map.transform;

                currentBlockZ = 0;
                string[] fileLines = File.ReadAllLines(readFromFilePath + "/" + levelFile.Name);
                foreach (string line in fileLines)
                {
                    //more arrangement of the rows to keep hierarchy clean
                    GameObject row = new GameObject();
                    row.transform.name = "Row " + currentBlockZ;
                    row.transform.parent = YSlice.transform;

                    currentBlockX = 0;

                    foreach (char character in line)
                    {
                        GameObject blockToInstantiate = null;

                        foreach (Block block in prefabs)
                        {
                            if (character == block.character) {
                                blockToInstantiate = Instantiate(block.prefab);
                                blockToInstantiate.name = block.prefab.name;

                                blockToInstantiate.transform.position =
                                    new Vector3(currentBlockX * GameManager.instance.blockSize,
                                    currentBlockY * GameManager.instance.blockSize,
                                    currentBlockZ * GameManager.instance.blockSize);
                                blockToInstantiate.transform.parent = row.transform;
                                blockToInstantiate.transform.name += "Block " + currentBlockX;
                                currentBlockX++;
                            }
                        }
                    }

                    currentBlockZ++;
                }

                currentBlockY++;
            }
        }
    }
}

[System.Serializable]
public class Block{
    [SerializeField]
    public char character;

    [SerializeField]
    public GameObject prefab;
}
