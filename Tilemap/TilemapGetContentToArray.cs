//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Get all tiles in a tilemap and sets all on an array")]

    public class TilemapGetContentToArray : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to get content from\nIgnored if a GameObject with a tilemap component is provided")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Store")]

        [ArrayEditor(typeof(Tile))]
        public FsmArray tileArray;

        private List<Tile> arrayTile;
        private Tile value;

        //Hides Tilemap variable option * via [HideIf("HideTilemap")] * if a GameObject with a Tilemap is provided
        private bool hideTilemap = false;
        public bool HideTilemap()
        {
            if (tilemapObject.Value != null)
                hideTilemap = true;
            else
                hideTilemap = false;
            return hideTilemap;
        }

        //Checks for required variables
        public override string ErrorCheck()
        {
            if (tilemapObject.Value == null && tilemap.Value == null)
                return "Either a Tilemap or a GameObject with a Tilemap is required.";

            return "";
        }

        //Reset
        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            tileArray = new FsmArray { UseVariable = true };
            arrayTile = null;
            value = null;
        }

        //On Enter
        public override void OnEnter()
        {
            //Error Debug Log
            if (tilemapObject.Value == null && tilemap.Value == null)
            {
                Debug.LogWarning("Either a Tilemap or a GameObject with a Tilemap is required." + " @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            if (tilemapObject.Value != null)
                tilemap = tilemapObject.Value.GetComponent<Tilemap>();

            Action();

            Finish();
        }

        //Action
        void Action()
        {
            var map = tilemap.Value as Tilemap;

            foreach (var position in map.cellBounds.allPositionsWithin)
            {
                arrayTile.Add(map.GetTile(position) as Tile);

                tileArray.Resize(tileArray.Length + 1);

                tileArray.Set(tileArray.Length - 1, value);
            }

        }
    }  
}
