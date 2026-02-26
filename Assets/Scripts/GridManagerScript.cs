using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManagerScript : MonoBehaviour
{
    private int _width, _height;

    [SerializeField] private Tile _tileprefab;

    [SerializeField] private Transform _camera;
    ObjectManagerScript ObjectManager;
    private void Start()
    {
        GenerateGrid(16, 9);
    }

    private void Update()
    {
        ////for later to change tile color when placing objects
        //if (ObjectManager.PlacementMode == true)
        //{
        //    var tiles = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
        //    foreach (var changeTile in tiles)
        //    {

        //    }
        //}
    }

    public void GenerateGrid(int _width, int _height)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tileprefab, new Vector3(x, y, 1), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";
            }
        }

        _camera.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height/2 - 0.5f, -100);
    }
    public void DeleteGrid()
    {
        var tiles = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (var deleteTile in tiles)
            {
                Destroy(deleteTile.gameObject);
        }
    }
}
