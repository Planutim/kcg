//imports UnityEngine

using Utility;

namespace KGUI
{
    public class PipeElementUI : ElementUI, IToggleElement
    {
        [UnityEngine.SerializeField] private ImageWrapper border;

        public override void Init()
        {
            base.Init();

            HitBoxObject = border.UnityImage.gameObject;
            
            ID = ElementEnums.PipePT;
            
            icon.Init(16, 16,"Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png", Enums.AtlasType.Gui);
            border.Init(GameState.GUIManager.WhiteSquareBorder);
        }

        public override void Draw()
        {
            icon.Draw();
            border.Draw();
        }

        public override void OnMouseStay()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = !gameObject.activeSelf;
        }
        
        public override void OnMouseExited()
        {
            GameState.GUIManager.SelectedInventoryItem.itemTile.InputsActive = true;
        }

        public override void OnMouseClick()
        {
            var item = GameState.GUIManager.SelectedInventoryItem;

            if (item != null)
            {
                item.itemTile.TileID = Enums.PlanetTileMap.TileID.Pipe;
                Toggle(true);
            }
        }
        
        public void Toggle(bool value)
        {
            border.UnityImage.color = value ? UnityEngine.Color.red : UnityEngine.Color.yellow;
        }
    }
}
