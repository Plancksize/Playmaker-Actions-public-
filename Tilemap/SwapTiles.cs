//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tilemap")]
	[Tooltip("Swaps all existing tiles of changeTile to newTile and refreshes all the swapped tiles.")]

	public class SwapTiles : FsmStateAction
	{
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to find the Tile\nIgnored if a GameObject with a tilemap component is provided")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Tiles")]

        [Tooltip("")]
        [ObjectType(typeof(Tile))]
        public FsmObject originalTile;

        [Tooltip("")]
        [ObjectType(typeof(Tile))]
        public FsmObject newTile;

        private Tilemap map;

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

        //On Reset
        public override void Reset()
		{
            tilemap = new FsmObject { UseVariable = true };
            tilemapObject = new FsmGameObject { UseVariable = true };
            originalTile = new FsmObject { UseVariable = true };
            newTile = new FsmObject { UseVariable = true };
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

            Action();

            Finish();
        }

        //Action
        void Action()
        {
            if (tilemapObject.Value != null)
                tilemap = tilemapObject.Value.GetComponent<Tilemap>();

            map = tilemap.Value as Tilemap;

            map.SwapTile(originalTile.Value as Tile, newTile.Value as Tile);
        }
    }
}
