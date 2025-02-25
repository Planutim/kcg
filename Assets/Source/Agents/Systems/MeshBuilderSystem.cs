﻿//import UnityEngine

namespace Agent
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("AgentsGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Agent), drawOrder);
        }

        public void UpdateMesh()
        {
            var AgentsWithSprite = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentSprite2D));

            int index = 0;
            Mesh.Clear();
            foreach (var entity in AgentsWithSprite)
            {
                int spriteId = entity.agentSprite2D.SpriteId;

                if (entity.hasAnimationState)
                {
                    var animation = entity.animationState;
                    spriteId = animation.State.GetSpriteId();
                }

                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.Agent).TextureCoords;

                var x = entity.agentPhysicsState.Position.X;
                var y = entity.agentPhysicsState.Position.Y;
                var width = entity.agentSprite2D.Size.X;
                var height = entity.agentSprite2D.Size.Y;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);
                // Update Vertices
                Mesh.UpdateVertex((index * 4), x, y, width, height);
                index++;
            }
        }
    }
}
