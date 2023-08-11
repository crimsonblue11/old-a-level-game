using UnityEngine;
using UnityEngine.UI;

public class BlockInst : MonoBehaviour
{
    private GameObject cube;
    private GameObject cubeList;
    private GameObject BlockTxt;


    [SerializeField] private int cubeNum = 5;
    [SerializeField] private float stageSize = 20;

    private float[][] coords;
    private float x;
    private float z;

    void Start()
    {
        cube = (GameObject)Resources.Load("prefabs/Block", typeof(GameObject));
        cubeList = GameObject.Find("Blocks");
        BlockTxt = GameObject.Find("BlockTxt");

        coords = new float[cubeNum][];
        for (int i = 0; i < cubeNum; i++)
        {
            coords[i] = new float[2];
            do
            {
                coords[i][0] = Random.Range(-stageSize, stageSize);
                coords[i][1] = Random.Range(-stageSize, stageSize);
            } while (CheckCoords(coords, i) == false);

            Instantiate(cube, new Vector3(coords[i][0], 2.56f /* const height */, coords[i][1]), Quaternion.identity, cubeList.transform);
        }
    }

    private void Update()
    {
        BlockTxt.GetComponent<Text>().text = "Blocks left: " + cubeList.transform.childCount;
    }

    private bool CheckCoords(float[][] arr, int index)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == arr[index] && i != index)
            {
                return false;
            }
        }
        return true;
    }
}
