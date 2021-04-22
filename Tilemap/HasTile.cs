//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Returns whether there is a tile at the position.")]

    public class HasTile : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [Tooltip("Tilemap to find the Tile\nIgnored if a GameObject with a tilemap component is provided")]
        [ObjectType(typeof(Tilemap))]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Position")]

        [Tooltip("V3 Cell position to find the Tile")]
        [Title("Cell Position")]
        public FsmVector3 position;

        [Tooltip("X position to find the Tile. Offsets Vector3 if provided")]
        [Title("Cell Position X")]
        public FsmInt posX;

        [Tooltip("Y position to find the Tile. Offsets Vector3 if provided")]
        [Title("Cell Position Y")]
        public FsmInt posY;

        [Tooltip("Z position to find the Tile. Offsets Vector3 if provided")]
        [Title("Cell Position Z")]
        public FsmInt posZ;

        [ActionSection("Result")]

        [Title("Store Bool")]
        [Tooltip("True if a Tile is found")]
        public FsmBool hasTile;

        [ActionSection("Send Events")]

        [Tooltip("Event to send if a Tile is found")]
        public FsmEvent isTrue;

        [Tooltip("Event to send if no Tile is found")]
        public FsmEvent isFalse;

        [ActionSection("On Update")]

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        private Tilemap map;
        private Vector3Int positionInt;

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
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            position = new FsmVector3 { UseVariable = true };
            posX = new FsmInt { UseVariable = true };
            posY = new FsmInt { UseVariable = true };
            posZ = new FsmInt { UseVariable = true };
            hasTile = new FsmBool { UseVariable = true };
            map = null;
            everyFrame = false;
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

            if (!everyFrame)
            {
                Finish();
            }
        }

        //On Update
        public override void OnUpdate()
        {
            Action();
        }

        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (position.IsNone)
                positionInt = new Vector3Int(posX.Value, posY.Value, posZ.Value);
            else
                positionInt = new Vector3Int(Mathf.RoundToInt(position.Value.x + posX.Value), Mathf.RoundToInt(position.Value.y + posY.Value), Mathf.RoundToInt(position.Value.z) + posZ.Value);
            

            hasTile.Value = map.HasTile(positionInt);

            Fsm.Event(hasTile.Value ? isTrue : isFalse);
        }
    }
}