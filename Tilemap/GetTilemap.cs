//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Tilemaps;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tilemap")]
	[Tooltip("Gets the Tilemap Component from a GameObject.")]

	public class GetTilemap : FsmStateAction
	{
        [ActionSection("GameObject")]

        [RequiredField]
        [Tooltip("Tilemap Owner")]
        [Title("Tilemap Owner")]
        [CheckForComponent(typeof(Tilemap))]
        public FsmGameObject tilemapOwner;

        [ActionSection("Result")]
        //[RequiredField]
        [Tooltip("Tilemap")]
        [ObjectType(typeof(Tilemap))]
        public FsmObject tilemap;

        //On Reset
        public override void Reset()
		{
            tilemap = new FsmObject { UseVariable = true };
            tilemapOwner = new FsmGameObject { UseVariable = true };
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
            tilemap.Value = tilemapOwner.Value.GetComponent<Tilemap>();
        }
    }
}
