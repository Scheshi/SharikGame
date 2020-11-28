using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SharikGame
{
    public class AddingBonus : Bonus
    {
        [SerializeField] private PlayerModel _plusingModel;
        protected override void Interaction(PlayerView playerView)
        {
            playerView.GetController.Adjust(_plusingModel);
        }
    }
}
