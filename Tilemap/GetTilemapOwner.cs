//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tilemap")]
	[Tooltip("Gets the Owner(GameObject) of a tilemap Object.")]

	public class GetTilemapOwner : FsmStateAction
	{
        [ActionSection("Tilemap")]

        [RequiredField]
        [Tooltip("Tilemap")]
        [ObjectType(typeof(Tilemap))]
        public FsmObject tilemap;

        [ActionSection("Result")]

        //[RequiredField]
        [Tooltip("Tilemap Owner")]
        [Title("Tilemap Owner")]
        public FsmGameObject tilemapOwner;

        private Tilemap map;

        //On Reset
        public override void Reset()
		{
            tilemap = new FsmObject { UseVariable = true };
            tilemapOwner = new FsmGameObject { UseVariable = true };
            map = null;
        }

        //On Enter
	public override void OnEnter()
        {   
            Action();

            Finish();
        }

        //Action
        void Action()
        {
            map = tilemap.Value as Tilemap;

            tilemapOwner.Value = map.gameObject;          
        }
    }
}
