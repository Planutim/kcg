using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;

namespace Planet.Unity
{
    class PlanetTest : MonoBehaviour
    {
        [SerializeField] Material Material;

        public PlanetState Planet;
        Inventory.InventoryManager inventoryManager;
        Inventory.DrawSystem inventoryDrawSystem;

        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;
        int toolBarID;

        public static int HumanoidCount = 1;
        GameObject[] HumanoidArray;

        AnimationClip IdleAnimationClip ;
        AnimationClip RunAnimationClip ;
        AnimationClip WalkAnimationClip ;
        AnimationClip GolfSwingClip;

        AnimancerComponent[] AnimancerComponentArray;

        HUDManager hudManager;

        static bool Init = false;

        public void Start()
        {
            if (!Init)
            {
                Initialize();
                Init = true;
            }
        }

        public void Update()
        {
            bool run = Input.GetKeyDown(KeyCode.R);
            bool walk = Input.GetKeyDown(KeyCode.W);
            bool idle = Input.GetKeyDown(KeyCode.I);
            bool golf = Input.GetKeyDown(KeyCode.G);

            for (int i = 0; i < HumanoidCount; i++)
            {
                if (run)
                {
                    AnimancerComponentArray[i].Play(RunAnimationClip, 0.25f);
                }
                else if (walk)
                {
                    AnimancerComponentArray[i].Play(WalkAnimationClip, 0.25f);
                }
                else if (idle)
                {
                    AnimancerComponentArray[i].Play(IdleAnimationClip, 0.25f);
                }
                else if (golf)
                {
                    AnimancerComponentArray[i].Play(GolfSwingClip, 0.25f);
                }
            }

            int toolBarID = Player.agentToolBar.ToolBarID;
            InventoryEntity Inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryID(toolBarID);
            int selectedSlot = Inventory.inventorySlots.Selected;

            ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext.itemInventory, toolBarID, selectedSlot);
            if (item != null)
            {
                ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);
                if (itemProperty.IsTool())
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType,
                            Player.agentID.ID, item.itemID.ID);
                    }   
                }
            }
            
            Planet.Update(Time.deltaTime, Material, transform);
            //   Vector2 playerPosition = Player.Entity.agentPosition2D.Value;

            // transform.position = new Vector3(playerPosition.x - 6.0f, playerPosition.y - 6.0f, -10.0f);
        }

        private void OnGUI()
        {
            if (!Init)
                return;

            if (Event.current.type != EventType.Repaint)
                return;

            // Draw HUD UI
            hudManager.Update(Player);

            // Draw Statistics
            KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);

            inventoryDrawSystem.Draw(Planet.EntitasContext, transform);
        }

        private void OnDrawGizmos()
        {
            // Set the color of gizmos
            Gizmos.color = Color.green;
            
            // Draw a cube around the map
            if(Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPosition2D.Value.X -10.0f >= Planet.TileMap.MapSize.X)
                {
                    // Out of bounds
                
                    // X+
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X + 10.0f, Player.agentPosition2D.Value.Y));

                    // X-
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X - 10.0f, Player.agentPosition2D.Value.Y));

                    // Y+
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y + 10.0f));

                    // Y-
                    Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            Admin.AdminAPI.DrawChunkVisualizer(Planet.TileMap);

            // Draw Selected Tiles
            var entities = Planet.EntitasContext.actionProperties.GetGroup(ActionPropertiesMatcher.AllOf(ActionPropertiesMatcher.ActionPropertyTilePlacement));
            foreach (var entity in entities)
            {
                Admin.AdminAPI.DrawSelectedTiles(entity.actionPropertyTilePlacement.TilesPos, ref Planet);
            }
        }

        // create the sprite atlas for testing purposes
        public void Initialize()
        {
            // get the 3d model from the scene
            //GameObject humanoid = GameObject.Find("DefaultHumanoid");

            GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);

            HumanoidArray = new GameObject[HumanoidCount];
            AnimancerComponentArray = new AnimancerComponent[HumanoidCount];

            for(int i = 0; i < HumanoidCount; i++)
            {
                HumanoidArray[i] = Instantiate(prefab);
                HumanoidArray[i].transform.position = new Vector3(5.0f, 20.0f, -1.0f);

                Vector3 eulers = HumanoidArray[i].transform.rotation.eulerAngles;
                HumanoidArray[i].transform.rotation = Quaternion.Euler(0, eulers.y + 90, 0);
                
            }


            // create an animancer object and give it a reference to the Animator component
            for(int i = 0; i < HumanoidCount; i++)
            {
                GameObject animancerComponent = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
                // get the animator component from the game object
                // this component is used by animancer
                AnimancerComponentArray[i] = animancerComponent.GetComponent<AnimancerComponent>();
                AnimancerComponentArray[i].Animator = HumanoidArray[i].GetComponent<Animator>();
            }

            
            IdleAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
            RunAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
            WalkAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Walk);
            GolfSwingClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);


            // play the idle animation
            for(int i = 0; i < HumanoidCount; i++)
            {
                AnimancerComponentArray[i].Play(IdleAnimationClip);
            }

            Application.targetFrameRate = 60;

            inventoryManager = new Inventory.InventoryManager();
            inventoryDrawSystem = new Inventory.DrawSystem();

            GameResources.Initialize();

            // Generating the map
            Vec2i mapSize = new Vec2i(32, 32);
            Planet = new Planet.PlanetState();
            Planet.Init(mapSize);

            Planet.InitializeSystems(Material, transform);
            
            /*var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
            Planet.TileMap = TileMapManager.Load("map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);*/

            GenerateMap();
            SpawnStuff();

            //TileMapManager.Save(Planet.TileMap, "map.kmap");

            inventoryID = Player.agentInventory.InventoryID;
            toolBarID = Player.agentToolBar.ToolBarID;

            hudManager = new HUDManager(Planet, Player);

            // Admin API Spawn Items
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol, Planet.EntitasContext);
            Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore, Planet.EntitasContext);

            // Admin API Add Items
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.PlacementToolBack, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.RemoveTileTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.SpawnEnemySlimeTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.MiningLaserTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.PipePlacementTool, Planet.EntitasContext);
            Admin.AdminAPI.AddItem(inventoryManager, toolBarID, Enums.ItemType.ParticleEmitterPlacementTool, Planet.EntitasContext);
        }

        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var tileMap = ref Planet.TileMap;

            for (int j = 0; j < tileMap.MapSize.Y; j++)
            {
                for (int i = 0; i < tileMap.MapSize.X; i++)
                {
                    var frontTileID = TileID.Air;
                    var backTileID = TileID.Air;

                    if (i >= tileMap.MapSize.X / 2)
                    {
                        if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                        {
                            frontTileID = TileID.Moon;
                            backTileID = TileID.Background;
                        }
                        else
                        {
                            frontTileID = TileID.Glass;
                            backTileID = TileID.Background;
                        }
                    }
                    else
                    {
                        if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                        {
                            frontTileID = TileID.Glass;
                            backTileID = TileID.Background;
                        }
                        else
                        {
                            frontTileID = TileID.Moon;
                            backTileID = TileID.Background;
                            if ((int) KMath.Random.Mt19937.genrand_int32() % 10 == 0)
                            {
                                int oreRandom = (int) KMath.Random.Mt19937.genrand_int32() % 3;
                                if (oreRandom == 0)
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameResources.OreSprite;
                                }
                                else if (oreRandom == 1)
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameResources.Ore2Sprite;
                                }
                                else
                                {
                                    tileMap.GetTile(i, j).CompositeTileSpriteID = GameResources.Ore3Sprite;
                                }

                                tileMap.GetTile(i, j).DrawType = TileDrawType.Composited;
                            }
                        }
                    }

                    tileMap.SetFrontTile(i, j, frontTileID);
                    tileMap.SetBackTile(i, j, backTileID);
                }
            }



            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                for (int j = tileMap.MapSize.Y - 10; j < tileMap.MapSize.Y; j++)
                {
                    tileMap.SetFrontTile(i, j, TileID.Air);
                    tileMap.SetBackTile(i, j, TileID.Air);
                }
            }

            int carveHeight = tileMap.MapSize.Y - 10;

            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                int move = ((int) KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int) KMath.Random.Mt19937.genrand_int32() % 5) <= 3)
                {
                    move = 0;
                }

                carveHeight += move;
                if (carveHeight >= tileMap.MapSize.Y)
                {
                    carveHeight = tileMap.MapSize.Y - 1;
                }

                if (carveHeight < 0)
                {
                    carveHeight = 0;
                }

                for (int j = carveHeight; j < tileMap.MapSize.Y && j < carveHeight + 4; j++)
                {
                    tileMap.SetFrontTile(i, j, TileID.Air);
                    tileMap.SetBackTile(i, j, TileID.Air);
                    tileMap.SetMidTile(i, j, TileID.Wire);
                }
            }

            carveHeight = 5;

            for (int i = tileMap.MapSize.X - 1; i >= 0; i--)
            {
                int move = ((int) KMath.Random.Mt19937.genrand_int32() % 3) - 1;
                if (((int) KMath.Random.Mt19937.genrand_int32() % 10) <= 3)
                {
                    move = 1;
                }

                carveHeight += move;
                if (carveHeight >= tileMap.MapSize.Y)
                {
                    carveHeight = tileMap.MapSize.Y - 1;
                }

                if (carveHeight < 0)
                {
                    carveHeight = 0;
                }

                for (int j = carveHeight; j < tileMap.MapSize.Y && j < carveHeight + 4; j++)
                {
                    tileMap.GetTile(i, j).FrontTileID = TileID.Air;
                    tileMap.GetTile(i, j).MidTileID =  TileID.Pipe;
                }
            }


            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }

            var camera = Camera.main;
            Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

            tileMap.SetFrontTile(4, 15, TileID.Platform);
            tileMap.SetFrontTile(5, 15, TileID.Platform);
            tileMap.SetFrontTile(6, 15, TileID.Platform);
            tileMap.SetFrontTile(7, 15, TileID.Platform);
            tileMap.SetFrontTile(8, 15, TileID.Platform);

            tileMap.SetFrontTile(4, 18, TileID.Platform);
            tileMap.SetFrontTile(5, 18, TileID.Platform);
            tileMap.SetFrontTile(6, 18, TileID.Platform);
            tileMap.SetFrontTile(7, 18, TileID.Platform);
            tileMap.SetFrontTile(8, 18, TileID.Platform);

            tileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            tileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
        }

        void SpawnStuff()
        {
            ref var tileMap = ref Planet.TileMap;
            System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

            float spawnHeight = tileMap.MapSize.Y - 2;

            Player = Planet.AddPlayer(new Vec2f(3.0f, spawnHeight));
            PlayerID = Player.agentID.ID;

            Planet.AddAgent(new Vec2f(6.0f, spawnHeight));
            Planet.AddAgent(new Vec2f(1.0f, spawnHeight));

            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                if (random.Next() % 5 == 0)
                {
                    Planet.AddEnemy(new Vec2f((float)i, spawnHeight));    
                }
            }
            
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Pistol, new Vec2f(6.0f, spawnHeight));
            GameState.ItemSpawnSystem.SpawnItemParticle(Planet.EntitasContext, Enums.ItemType.Ore, new Vec2f(10.0f, spawnHeight));
        }
    }
}
