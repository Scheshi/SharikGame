using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SharikGame
{
    public class AddingBonus : Bonus
    {
        [SerializeField] private PlayerStruct _plusingModel;
        protected override void Interaction(PlayerView playerView)
        {
            playerView.Model.Adjust(_plusingModel);
        }
    }
}
