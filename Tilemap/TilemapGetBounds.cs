//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Gets the Origin and Size of a Tilemap in Cells")]

    public class TilemapGetBounds : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [ObjectType(typeof(Tilemap))]
        [Tooltip("Tilemap to get size\nIgnored if a GameObject with a tilemap component is provided")]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Result")]

        [Title("Tilemap Origin")]
        [Tooltip("Origin point of the Tilemap")]
        public FsmVector3 boundsPosition;

        [ActionSection("")]

        [Title("Tilemap Size")]
        [Tooltip("Size (in Cells) of the Tilemap")]
        public FsmVector3 boundsSize;

        [Tooltip("Tilemap Size in the X Axis")]
        [Title("Tilemap Size X")]
        public FsmInt sizeX;

        [Tooltip("Tilemap Size in the Y Axis")]
        [Title("Tilemap Size Y")]
        public FsmInt sizeY;

        [Tooltip("Tilemap Size in the Z Axis")]
        [Title("Tilemap Size Z")]
        public FsmInt sizeZ;

        private BoundsInt bounds;
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

        //Reset
        public override void Reset()
        {
            tilemapObject = new FsmGameObject { UseVariable = true };
            tilemap = new FsmObject { UseVariable = true };
            boundsPosition = new FsmVector3 { UseVariable = true };
            boundsSize = new FsmVector3 { UseVariable = true };
            sizeX = new FsmInt { UseVariable = true };
            sizeY = new FsmInt { UseVariable = true };
            sizeZ = new FsmInt { UseVariable = true };
            bounds = new BoundsInt();
            map = null;
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

            map = tilemap.Value as Tilemap;

            Action();

            Finish();
        }

        //Action
        void Action()
        {
            bounds = new BoundsInt(map.cellBounds.position, map.cellBounds.size);

            boundsPosition.Value = bounds.position;

            boundsSize.Value = bounds.size;
            sizeX.Value = bounds.size.x;
            sizeX.Value = bounds.size.y;
            sizeX.Value = bounds.size.z;
        }
    }
}