using System;
using System.Collections.Generic;
using System.Linq;
using EquestriEngine.Data.Inputs;
using EquestriEngine.Data.Inputs.Interfaces;
namespace ElegyGame
{
    public class ElegyOfDisharmonyGlobals : EquestriEngine.EngineGlobals
    {
        /// <summary>
        /// Starts a battle sequence
        /// </summary>
        /// <param name="sender">The object that sent the request </param>
        /// <param name="input">A BattleInput, this will hold information on enemies in battle, 
        /// and spoils, as well as info about the type of battle</param>
        public static MethodResult InitiateBattle(object sender, IEventInput input)
        {
            if (sender is GameData.Map.Character.Enemy.Enemy)
            {

            }
            var bScreen = new Screens.BattleScreen();
            StateManager.AddScreenLoad(bScreen);
            return MethodResult.Success;
        }
        
    }
}