using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileProperties
{
    class TilePropertiesManager
    {
        public PlanetTileProperties[] TileProperties;
        
        //SingleTon
        private static TilePropertiesManager _instance;
       // public static TilePropertiesManager Instance;
        public static TilePropertiesManager Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new TilePropertiesManager();
                return _instance;
            }
        }
            //Todo: Move Init to Init Function

            /*
            if (instance == null)
            {
                Instance = new TilePropertiesManager();
            }
            return Instance; 
            */


        public static void InitStage1()
        {
            //This is where init goes
            //Instance = new TilePropertiesManager();
        }

        public static void InitStage2()
        {

        }
    }
}
    //TODO: add a function to get pointer to TileProperty struct from index
