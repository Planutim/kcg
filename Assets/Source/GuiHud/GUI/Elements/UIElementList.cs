using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace KGUI.Elements
{
    public class UIElementList
    {
        
        // array for storing entities
        public UIElementEntity[] List;

        public int Size;
        // used for tracking down an available 
        // index that we can use to insert
        public int LastFreeIndex;

        // the capacity is just the length of the list
        public int Capacity
        {
            get
            {
                return List.Length;
            }
        }

        public UIElementList()
        {
            List = new UIElementEntity[1024];
        }


        public UIElementEntity Add(UIElementEntity entity)
        {
            // if we dont have enough space we expand
            // the capacity
            if (Size + 1 >= Capacity)
            {
                Expand(ExpandFunction(Capacity));
            }


            // trying to find an empty index
            // we use LastFreeIndex for a faster insertion
            int Found = -1;
            for(int index = LastFreeIndex; index < Capacity; index++)
            {
                UIElementEntity thisEntity = List[index];

                if (thisEntity == null)
                {
                    Found = index;
                    break;
                }
            }
            if (Found == -1)
            {
                for(int index = 0; index < LastFreeIndex; index++)
                {
                    UIElementEntity thisEntity = List[index];

                    if (thisEntity == null)
                    {
                        Found = index;
                        break;
                    }
                }
            }

            // increment the LastFreeIndex
            LastFreeIndex = (LastFreeIndex + 1) % Capacity;


            // creating the Entity and initializing it
            entity.ReplaceKGUIElementsID(Found);
            List[Found] = entity;
            Size++;

             return List[Found];
        }


        public UIElementEntity Get(int Index)
        {
            return List[Index];
        }


        // to remove an entity we just 
        // set the IsInitialized field to false
        public void Remove(int floatingTextId)
        {
            LastFreeIndex = floatingTextId;
            ref UIElementEntity entity = ref List[floatingTextId];
            entity.Destroy();
            entity = null;
            Size--;
        }




        // used to grow the list
        private void Expand(int NewCapacity)
        {
            // make sure the new capacity is more than 1
            if (NewCapacity == 0)
            {
                NewCapacity = 1;
            }

            // make sure the new capacity 
            // is bigget than the old one
            if (NewCapacity > Capacity)
            {
                System.Array.Resize(ref List, Capacity);
            }
        }
        

        // We use this to determine 
        // the new size based off the old one.
        // The new size should allways be bigger 
        private int ExpandFunction(int oldSize)
        {
            return oldSize * 2;
        }
    }
}