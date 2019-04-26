using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using TiledSharp;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    private int currentLevelIndex = -1;
    [SerializeField]
    private LevelConfig levelConfig = null;
    [SerializeField]
    private EnemySpawnConfig enemySpawnConfig = null;
    [SerializeField]
    private Texture2D spriteAtlas = null;
    private Sprite[] sprites = null;
    [SerializeField]
    private Tile tilePrefab = null;
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private EndTrigger endPrefab = null;
    [SerializeField]
    private LevelRuntime levelRuntime = null;
    [SerializeField]
    private PlayerRuntime playerRuntime = null;

    private GameObject background = null;
    private GameObject colliders = null;
    private GameObject enemies = null;
    private GameObject objects = null;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Tilesets/" + spriteAtlas.name);
        GameObject empty = new GameObject();
        background = Instantiate(empty, transform);
        background.name = "Background";
        colliders = Instantiate(empty, transform);
        colliders.name = "Colliders";
        enemies = Instantiate(empty, transform);
        enemies.name = "Enemies";
        objects = Instantiate(empty, transform);
        objects.name = "Objects";
        Destroy(empty);
        levelRuntime.LoadNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO this is game manager stuff
        if (levelRuntime.LoadNext)
        {
            Debug.Log("loading! "+Time.time);
            levelRuntime.LoadNext = false;
            //init game
            currentLevelIndex++;
            if(currentLevelIndex == levelConfig.Levels.Count)
            {
                //TODO win!
                return;
            }
            foreach (Transform child in background.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in colliders.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in enemies.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in objects.transform)
            {
                Destroy(child.gameObject);
            }
            LoadLevel(levelConfig.Levels[currentLevelIndex].text, levelConfig.Tileset.text);
        }
    }

    public void LoadLevel(string level, string tilesetRef)
    {
        XDocument doc = XDocument.Parse(level);
        XDocument tilesetDoc = XDocument.Parse(tilesetRef);
        TmxMap map = new TmxMap(doc);
        map.AddTilesetsAsXContainers(tilesetDoc.Elements("tileset").ToList());
        TmxTileset tileset = map.Tilesets[0];

        float maxX = 0;
        int layerInt = 0;
        foreach (TmxLayer layer in map.Layers)
        {
            bool disableCollider = layer.Name != "colliders" && layer.Name != "objects";
            GameObject parent = background;
            if (layer.Name == "enemyspawns")
            {
                parent = enemies;
            }
            else if (layer.Name == "playertriggers")
            {
                parent = objects;
            }
            else if (!disableCollider)
            {
                parent = colliders;
            }

            foreach (TmxLayerTile tile in layer.Tiles)
            {
                if (tile.Gid <= 0 || !tileset.Tiles.ContainsKey(tile.Gid)) continue;
                TmxTilesetTile tilesetTile = tileset.Tiles[tile.Gid - 1];

                if (layer.Name == "enemyspawns")
                {
                    Enemy e = Instantiate(
                        enemySpawnConfig.SpawnIdlist.Where(
                            x => x.key == tilesetTile.Id
                            ).FirstOrDefault().enemy,
                        parent.transform);
                    e.transform.position = new Vector3(tile.X - 10, -tile.Y + 8f, 0);
                }
                else if (layer.Name == "playertriggers")
                {
                    if (tilesetTile.Id == 344)
                    {
                        player.transform.position = new Vector3(tile.X - 10, -tile.Y + 8f, 0);
                        playerRuntime.Position = new Vector3(0.1f, 0, 0);
                    }
                    if (tilesetTile.Id == 343)
                    {
                        EndTrigger e = Instantiate(endPrefab, parent.transform);
                        e.transform.position = new Vector3(tile.X - 10.5f, -tile.Y + 8f, 0);
                        e.LevelRuntime = levelRuntime;
                    }
                }
                else
                {
                    Tile tileObj = Instantiate(tilePrefab, parent.transform);
                    SpriteRenderer renderer = tileObj.GetComponent<SpriteRenderer>();
                    renderer.sprite = sprites[tile.Gid - 1];
                    if(layer.Name == "enemyboundaries")
                    {
                        renderer.enabled = false;
                    }
                    float xPos = tile.X - 10;
                    tileObj.transform.position = new Vector3(xPos, -tile.Y + 8.5f, 0);
                    tileObj.gameObject.layer = LayerMask.NameToLayer(layer.Name);
                    tileObj.SetLayerInt(layerInt);
                    tileObj.name = tileObj.name + "-" + layer.Name;

                    tileObj.SetCollider(tilesetTile.Properties["collider"]);
                    maxX = Mathf.Max(maxX, xPos);
                }
            }
            layerInt++;
        }

        foreach (TmxObjectGroup objectGroup in map.ObjectGroups)
        {
            foreach (TmxObject obj in objectGroup.Objects)
            {
                TmxLayerTile tile = obj.Tile;
                float tileX = (float)obj.X / 8;
                float tileY = (float)obj.Y / 8;
                TmxTilesetTile tilesetTile = tileset.Tiles[tile.Gid - 1];

                Tile tileObj = Instantiate(tilePrefab, objects.transform);
                SpriteRenderer renderer = tileObj.GetComponent<SpriteRenderer>();
                renderer.sprite = sprites[tile.Gid - 1];

                float xPos = tileX - 10;
                tileObj.transform.position = new Vector3(xPos, -tileY + 9.5f, 0);
                tileObj.gameObject.layer = LayerMask.NameToLayer("objects");
                tileObj.SetLayerInt(layerInt);
                tileObj.name = tileObj.name + "-" + objects;

                tileObj.SetCollider(tilesetTile.Properties["collider"]);
                maxX = Mathf.Max(maxX, xPos);
            }
        }

        levelRuntime.LevelWidth = maxX - 1f; // one smaller so the last wall stays hidden
    }
}
