using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject[] platforms;
    [SerializeField]
    private GameObject finishPlatform;
    public List<GameObject> platformCreated = new List<GameObject>();
    public int numberofPlatformsGenerated = 0;
    public int generatedPlatformsNumber = 5;
    private int currentPaternIndex = 0;
    private int currentPlatformIndex = 0;
    private List<int> paternsArray;
    private bool stopBuilding = false;
    private const float firstZOffset = 18f;
    private const float offsetPlatform = 6f;
    private const float yPosition = -7.9f;
    private const float xPosition = 0f;
    private const float zPosition = 0f;
    
    void Start()
    {
        buildLevel();
    }

    public void buildLevel()
    {
        stopBuilding = false;
        numberofPlatformsGenerated = 0;
        cleanWorld();
       
        initializePaterns();
        generateFirstPlatform();
        generateSecondPlatform();
        for (int i = 0; i < generatedPlatformsNumber; i++)
        {
            generatePlatform();
        }
        numberofPlatformsGenerated--;

    }

    void cleanWorld()
    {
        for (int i = 0; i < platformCreated.Count; i++)
        {
            Destroy(platformCreated[i]);
        }
        platformCreated = new List<GameObject>();
    }

    public void cleanOutPlatforms()
    {
        if (platformCreated.Count > generatedPlatformsNumber +1)
        {
            Destroy(platformCreated[0],1f);
            platformCreated.RemoveAt(0);
        }
    }

   

    int getRandomPlatformIndex()
    {
        int index;
        index = Random.Range(1, platforms.Length);
        return index;
    }

    void initializePaterns()
    {
        paternsArray = new List<int>();
        for (int i = 1; i < platforms.Length; i++)
        {
            paternsArray.Add(i);
        }
    }

    void createNewPlatform(int index)
    {

        GameObject lastPlatform = getLastPlatform();
        float lastPlatformZPosition = lastPlatform.transform.GetChild(lastPlatform.transform.childCount -1).transform.localPosition.z;
        float z = lastPlatform.transform.position.z + lastPlatformZPosition + offsetPlatform;
     
        Vector3 platformPosition = new Vector3(xPosition, yPosition, z);
        newPlatform(index, platformPosition);
    }

    void newPlatform(int index,Vector3 position)
    {
        GameObject platform;
        numberofPlatformsGenerated++;
       
        if (ScoreManager.GetPoint() >= LevelManager.GetTargetScore())
        {
            platform = finishPlatform;
            stopBuilding = true;
        }
        else
        {
            platform = platforms[index];
        }
       
        GameObject newPlatformCreated = Instantiate(platform, position,platform.transform.rotation) as GameObject;
        platformCreated.Add(newPlatformCreated);
    }

    void generateFirstPlatform()
    {
        int index = 0;
        GameObject platform = platforms[index];
        Vector3 vector = new Vector3(xPosition, yPosition, zPosition);
        newPlatform(index, vector);
    }

    void generateSecondPlatform()
    {
        int index = getRandomPlatformIndex();
        GameObject platform = platforms[index];
        Vector3 vector = new Vector3(xPosition, yPosition, firstZOffset);
        newPlatform(index, vector);
    }

    public void generatePlatform()
    {
        if (!stopBuilding)
        {
            if (currentPlatformIndex >= paternsArray.Count)
            {
                currentPaternIndex = getPaternIndex();
                currentPlatformIndex = 0;
            }

            createNewPlatform(getRandomPlatformIndex());
            currentPlatformIndex++;
        }
    }

    GameObject getLastPlatform()
    {
        int lastIndex = platformCreated.Count - 1;
        GameObject lastPlatform = platformCreated[lastIndex];

        return lastPlatform;
    }

    bool isLastPlatformSame(int index)
    {
        GameObject lastPlatform = getLastPlatform();
        GameObject platform = platforms[index];
        return platform.tag == lastPlatform.tag;
    }

    int getPaternIndex()
    {
        int index;
        index = Random.Range(0, paternsArray.Count);
        return index;
    }
}
