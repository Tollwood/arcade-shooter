using UnityEngine;

public class Navigation : MonoBehaviour {
    public Transform navMeshMaskPrefab;
    public Transform navMeshFloor;

    public void setup(Level currentMap, float tileSize, Transform mapHolder, Coord maxMapSize){
        Vector3 offSet = new Vector3(withUnEvanOffset(currentMap.mapSize.x), 0, withUnEvanOffset(currentMap.mapSize.y)) * tileSize;
        GetComponent<BoxCollider>().size = new Vector3(currentMap.mapSize.x * tileSize, 0.05f, currentMap.mapSize.y * tileSize);
        GetComponent<BoxCollider>().center = offSet;

        navMeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
        navMeshFloor.position = offSet;
        
        Vector3 positionLeft = Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + offSet;
        Transform maskLeft = Instantiate(navMeshMaskPrefab,positionLeft, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionRight = Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + offSet;
        Transform maskRight = Instantiate(navMeshMaskPrefab, positionRight, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionTop = Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + offSet;
        Transform maskTop = Instantiate(navMeshMaskPrefab, positionTop, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

        Vector3 positionBottom = Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize +offSet;
        Transform maskBottom = Instantiate(navMeshMaskPrefab, positionBottom, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y)/2f) * tileSize;

    }

    private float withUnEvanOffset(int value)
    {
        if (value % 2 == 1)
        {
            return 0.5f;
        }
        return 0;
    }
}
