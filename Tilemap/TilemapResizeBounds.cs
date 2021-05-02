//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Tilemap")]
    [Tooltip("Sets the Origin and Size of a Tilemap in Cells. Removes any tile outside the new bounderies.")]

    public class TilemapResizeBounds : FsmStateAction
    {
        [ActionSection("Tilemap")]

        [Tooltip("GameObject with the Tilemap")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapObject;

        [ObjectType(typeof(Tilemap))]
        [Tooltip("Tilemap to get size\nIgnored if a GameObject with a tilemap component is provided")]
        [HideIf("HideTilemap")]
        public FsmObject tilemap;

        [ActionSection("Set")]

        [Title("Tilemap Origin")]
        [Tooltip("New Origin point of the Tilemap")]
        public FsmVector3 boundsPosition;

        [ActionSection("")]

        [Title("Tilemap Size")]
        [Tooltip("New Size (in Cells) of the Tilemap")]
        public FsmVector3 boundsSize;

        [Tooltip("New Tilemap Size in the X Axis. Adds to Vector3 if provided")]
        [Title("Tilemap Size X")]
        public FsmInt sizeX;

        [Tooltip("New Tilemap Size in the Y Axis. Adds to Vector3 if provided")]
        [Title("Tilemap Size Y")]
        public FsmInt sizeY;

        [Tooltip("New Tilemap Size in the Z Axis. Adds to Vector3 if provided")]
        [Title("Tilemap Size Z")]
        public FsmInt sizeZ;

        private Vector3Int sizeInt;
        private Vector3Int newOrigin;
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
            if (tilemapObject.Value == null || tilemap.Value == null)
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
            sizeInt = new Vector3Int();
            newOrigin = new Vector3Int();
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

            Action();

            Finish();
        }


        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            if (boundsSize.IsNone)
                sizeInt = new Vector3Int(sizeX.Value, sizeY.Value, sizeZ.Value);
            else
                sizeInt = new Vector3Int(Mathf.RoundToInt(boundsSize.Value.x + sizeX.Value), Mathf.RoundToInt(boundsSize.Value.y + sizeY.Value), Mathf.RoundToInt(boundsSize.Value.z + sizeZ.Value));

            newOrigin = new Vector3Int(Mathf.RoundToInt(boundsPosition.Value.x), Mathf.RoundToInt(boundsPosition.Value.y), Mathf.RoundToInt(boundsPosition.Value.z));

            map.origin = newOrigin;
            map.size = sizeInt;

            map.ResizeBounds();
        }
    }
}