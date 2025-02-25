using Enums;
using Enums.PlanetTileMap;
using Utility;
//MOST IMPORTANT TILE

/*

CreateTile(TileId)
SetTileName("Regolith") //map from string to TileId
SetTileLayer(TileMapLayerBackground)
SetTileTexture(TileSpriteId, 2,10) //2nd row, 10th column of TileSpriteId
SetTilePropertyISExplosive(true)
SetTileDurability(60)
EndTile()

Atlas is a pixel array
Atlas starts empty
Sprites are copied to Atlas and we get a AtlasSpriteId

SetTileTexture(TileSpriteId, 2,10) //2nd row, 10th column of TileSpriteId
- What does this do?
-- It blits (copy) the Sprite from TileSpriteLoader (TileSpriteSheetId)
-- to the TileSpriteAtlas
-- AND get the AtlasSpriteId (index into the Atlas texture sheet)

SetTileId(5)
// TileType, TileLayer, Name
DefineTile(BlockTypeSolid, LayerForegound, "regolith");
SetTileTexture(ImageId2, 2,10); //2nd row, 10th column, of i
push_texture(); //some might have more than one

SetTilePropertyIsExplosive(true); //example
SetTileDurability(60);

SetTileTextDescription("Regolith is a kind of dust commonly found on the surface of astronomical objects,\n");
EndTile();
*/

namespace PlanetTileMap
{
    //https://github.com/kk-digital/kcg/issues/89

    //ALL TILES CREATED OR USED IN GAME HAVE TO BE CREATED HERE
    //ALL TILES ARE CREATED FROM FUNCTIONS IN THIS FILE
    //ALL SPRITES FOR TILES ARE SET AND ASSIGNED FROM THIS API

    public class TileCreationApi
    {
        // Start is called before the first frame update
        private TileID CurrentTileIndex;
        public TileProperty[] TilePropertyArray;
        
        public TileCreationApi()
        {
            var tilePropertyArray = new TileProperty[4096];

            for (int i = 0; i < tilePropertyArray.Length; i++)
            {
                tilePropertyArray[i].TileID = TileID.Error;
                tilePropertyArray[i].BaseSpriteId = -1;
            }

            TilePropertyArray = tilePropertyArray;
            
            CurrentTileIndex = TileID.Error;
        }

        public ref TileProperty GetTileProperty(Enums.MaterialType materialType, Enums.TileGeometryAndRotation shapeType)
        {
            for (int i = 0; i < TilePropertyArray.Length; i++)
            {
                if (TilePropertyArray[i].MaterialType == materialType && TilePropertyArray[i].BlockShapeType == shapeType)
                {
                    return ref TilePropertyArray[i];
                }
            }
            
            return ref TilePropertyArray[0];
        }
        
        public ref TileProperty GetTileProperty(TileID tileID)
        {
            return ref TilePropertyArray[(int)tileID];
        }

        public void CreateTileProperty(TileID tileID)
        {
            if (tileID == TileID.Error) return;

            CurrentTileIndex = tileID;
            TilePropertyArray[(int)CurrentTileIndex].TileID = tileID;
        }

        public void SetTileMaterialType(Enums.MaterialType materialType)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].MaterialType = materialType;
        }

        public void SetTilePropertyShape(Enums.TileGeometryAndRotation shape)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int) CurrentTileIndex].BlockShapeType = shape;
        }


        public void SetTilePropertySpriteSheet16(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
           

            if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R1 ||
                TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R2)
            {
                int baseId = 0;
                for(int j = column; j < column + 4; j++)
                {
                    for(int i = row; i < row + 4; i++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, i, j, 0);

                        // the first sprite id is the baseId
                        if (i == row && j == column)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R3)
            {
                int baseId = 0;
                for(int x = column; x < column + 5; x++)
                {
                    for(int y = row; y < row + 11; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else
            {
                int baseId = 0;
                for(int x = column; x < column + 1; x++)
                {
                    for(int y = row; y < row + 1; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
   
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
        }

        public void SetTilePropertySpriteSheet(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R1 ||
            TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R2)
            {
                int baseId = 0;
                
                for(int i = row; i <= row + 4; i++)
                {
                    for(int j = column; j <= column + 4; j++)
                    {
                        int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, i, j, 0);

                        // the first sprite id is the baseId
                        if (i == row && j == column)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }
                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
            else if (TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType == SpriteRuleType.R3)
            {
                int baseId = 0;
                for(int x = column; x < column + 5; x++)
                {
                    for(int y = row; y < row + 11; y++)
                    {
                        //FIX: Dont import GameState, make a method?
                        //TileAtlas is imported by GameState, so TileAtlas should not import GameState
                        int atlasSpriteId = 
                            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, y, x, 0);

                        // the first sprite id is the baseId
                        if (x == column && y == row)
                        {
                            baseId = atlasSpriteId;
                        }
                    }
                }

                TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = baseId;
                TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = true;
            }
        }

        public void SetTilePropertyTexture(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            //FIX: Dont import GameState, make a method?
            //TileAtlas is imported by GameState, so TileAtlas should not import GameState
            int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(spriteSheetId, row, column, 0);
            TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = atlasSpriteId;
            TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = false;
        }

        public void SetTilePropertyTexture16(int spriteSheetId, int row, int column)
        {
            if (CurrentTileIndex == TileID.Error) return;
              
            int atlasSpriteId = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(spriteSheetId, row, column, 0);
            TilePropertyArray[(int)CurrentTileIndex].BaseSpriteId = atlasSpriteId;
            TilePropertyArray[(int)CurrentTileIndex].IsAutoMapping = false;
            
        }

        public void SetTilePropertyCollisionType(CollisionType type)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].CollisionIsoType = type;
        }

        
        public void SetTilePropertyDurability(byte durability)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].Durability = durability;
        }

        public void SetTilePropertyDescription(byte durability)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            TilePropertyArray[(int)CurrentTileIndex].Durability = durability;
        }

        public void SetSpriteRuleType(SpriteRuleType spriteRuleType)
        {
            Utils.Assert((int)CurrentTileIndex >= 0 && (int)CurrentTileIndex < TilePropertyArray.Length);

            TilePropertyArray[(int)CurrentTileIndex].SpriteRuleType = spriteRuleType;
        }

        public void SetCannotBeRemoved(bool flag)
        {
            if (CurrentTileIndex == TileID.Error) return;
            
            TilePropertyArray[(int)CurrentTileIndex].CannotBeRemoved = flag;
        }

        public void SetDropTableID(int dropTableID)
        {
            if (CurrentTileIndex == TileID.Error) return;

            TilePropertyArray[(int)CurrentTileIndex].DropTableID = dropTableID;
        }

        public void EndTileProperty()
        {
            CurrentTileIndex = TileID.Error;
        }

        public int LoadingTilePlaceholderSpriteId;
        public int LoadingTilePlaceholderSpriteSheet;
        public int OreSpriteSheet;
        public int MoonSpriteSheet;
        public int StoneSpriteSheet;
        public int ColoredNumberedWangSpriteSheet;
        public int Ore2SpriteSheet;
        public int Ore3SpriteSheet;
        public int PipeSpriteSheet;
        public int PlatformSpriteSheet;

        public int MetalTileSheet;

        public int RockTileSheet;

        public int SQ_0;
        public int SQ_1;
        public int SQ_2;
        public int SQ_3;
        public int SQ_4;

        public int OreStoneSheet;
        public int OreStone_0;


        public void CreateMetalGeometryTiles()
        {
            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 19, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 21, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 23, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 25, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 1);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 19, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 21, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 23, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 25, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 10, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 12, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 14, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 16, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R4_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R5_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R6_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R7_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R0_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R1_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R2_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R3_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R4_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 1, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R5_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 3, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R6_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 5, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R7_Metal);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Metal);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(MetalTileSheet, 7, 15);
            GameState.TileCreationApi.EndTileProperty();
        }




        public void CreateRockGeometryTiles()
        {
           GameState.TileCreationApi.CreateTileProperty(TileID.FP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
                     GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R0);
            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R0_Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 19, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 21, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 23, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.FP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.FP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 25, 21);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.SB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 1);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HB_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 3);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 19, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 21, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 23, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.HP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.HP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 25, 19);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 10, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 12, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 14, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.QP_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 16, 17);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 5);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R2);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 7);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.TB_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.TB_R3);
            GameState.TileCreationApi.SetSpriteRuleType(PlanetTileMap.SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 7);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 9);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R4_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R5_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R6_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L1_R7_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L1_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 11);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R0_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R1_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R1);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R2_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R2);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R3_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R3);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 13);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R4_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R4);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 1, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R5_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R5);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 3, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R6_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R6);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 5, 15);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.L2_R7_Rock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Rock);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.L2_R7);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertyTexture(RockTileSheet, 7, 15);
            GameState.TileCreationApi.EndTileProperty();
        }

        public void InitializeResources()
        {
            LoadingTilePlaceholderSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\placeholder_loadingSprite.png", 32, 32);
            OreSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);
            MoonSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\Tiles_Moon.png", 16, 16);
            StoneSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Stone\\stone.png", 16, 16);
            ColoredNumberedWangSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\test - Copy.png", 16, 16);
            Ore2SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Copper\\ore_copper_1.png", 16, 16);
            Ore3SpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Adamantine\\ore_adamantine_1.png", 16, 16);
            PipeSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pipesim\\pipesim.png", 16, 16);
            PlatformSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Platform\\Platform1\\Platform_1.png", 48, 48);
            MetalTileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryMetal\\metal_tiles_geometry.png", 288, 736);
            RockTileSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\GeometryRock\\rock_tiles_geometry.png", 32, 32);
            OreStoneSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Gems-Ores\\Stone\\gems-ores-stone.png", 128, 128);

            LoadingTilePlaceholderSpriteId =
                    GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(LoadingTilePlaceholderSpriteSheet, 0, 0, 0);

            GameState.TileCreationApi.CreateTileProperty(TileID.Ore1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Ore1);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(OreSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Glass);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Glass);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 11, 10);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Stone);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Stone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetTilePropertySpriteSheet(StoneSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Moon);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Moon);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 0);
            int dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Moon, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.GoldBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 6);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Gold_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId); 
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.DiamondBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 7);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Diamond_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.LapisBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Lapis_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID. EmeraldBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.EmeraldBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 4);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Emerald_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.RedStoneBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 5);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.RedStone_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.IronBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 3);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Iron_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.CoalBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 1);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Coal_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_0);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 0, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_0, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_1);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 1, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_1, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 2, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_2, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 3, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_3, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_4);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 4, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_4, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_5);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 5, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_5, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_6);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 6, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_6, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.PinkDiaBlock_7);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.OreStone);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.NoRule);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(OreStoneSheet, 7, 2);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.PinkDia_7, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Background);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Background);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(ColoredNumberedWangSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore2);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Ore2);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore2SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();


            GameState.TileCreationApi.CreateTileProperty(TileID.Ore3);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Ore3);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetTilePropertyTexture16(Ore3SpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Pipe);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Pipe);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 0, 0);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Pipe, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Wire);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Wire);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R2);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(PipeSpriteSheet, 4, 12);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Wire, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Bedrock);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Bedrock);
            GameState.TileCreationApi.SetCannotBeRemoved(true);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.SB_R0);
            GameState.TileCreationApi.SetSpriteRuleType(SpriteRuleType.R3);
            GameState.TileCreationApi.SetTilePropertySpriteSheet16(MoonSpriteSheet, 0, 10);
            dropTableId = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Bedrock, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.TileCreationApi.SetDropTableID(dropTableId);
            GameState.TileCreationApi.EndTileProperty();

            GameState.TileCreationApi.CreateTileProperty(TileID.Platform);
            GameState.TileCreationApi.SetTileMaterialType(Enums.MaterialType.Moon);
            GameState.TileCreationApi.SetTilePropertyShape(Enums.TileGeometryAndRotation.QP_R0);
            GameState.TileCreationApi.SetTilePropertyCollisionType(CollisionType.Platform);
            GameState.TileCreationApi.SetTilePropertyTexture16(PlatformSpriteSheet, 0, 0);
            GameState.TileCreationApi.EndTileProperty();

            CreateMetalGeometryTiles();
            CreateRockGeometryTiles();
        }
    }
}
