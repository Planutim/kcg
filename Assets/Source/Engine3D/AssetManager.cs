using Animancer;
using UnityEngine;
using System;

namespace Engine3D
{


    //TODO(): allow assets to be loaded on gameplay instead of loading at start
    //TODO(): has to print the time to load all the assets
    public class AssetManager
    {

        public static AssetManager AssetManagerSingelton;

        public static AssetManager Singelton
        {
            get
            {
                if (AssetManagerSingelton == null)
                {
                    AssetManagerSingelton = new AssetManager();
                }

                return AssetManagerSingelton;
            }
        }




        AnimationLoader AnimationLoader;
        ModelLoader ModelLoader;
        MaterialLoader MaterialLoader;

        

        public AssetManager()
        {
            AnimationLoader = new AnimationLoader();
            ModelLoader = new ModelLoader();
            MaterialLoader = new MaterialLoader();

            long beginTime = DateTime.Now.Ticks;
            LoadMaterials();
            LoadAnimations();
            LoadModels();
            Debug.Log("3d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");
        }

        public ref AnimationClip GetAnimationClip(AnimationType animationType)
        {
            return ref AnimationLoader.GetAnimationClip(animationType);
        }

        public ref GameObject GetModel(ModelType modelType)
        {
            return ref ModelLoader.GetModel(modelType);
        }

        public ref Material GetMaterial(MaterialType materialType)
        {
            return ref MaterialLoader.GetMaterial(materialType);
        }

        private void LoadMaterials()
        {

        }

        private void LoadAnimations()
        {
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Idle", AnimationType.Idle);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Run", AnimationType.Run);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Walk_F", AnimationType.Walk);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump", AnimationType.Jump);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jump_Roll", AnimationType.Flip);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@Jog", AnimationType.Jog);
            AnimationLoader.Load("Shinabro/Platform_Animation/Animation/00_Base/Stander@JumpFall", AnimationType.JumpFall);
        }

        private void LoadModels()
        {
            ModelLoader.Load("Stander", ModelType.Stander);
        }

    }
}