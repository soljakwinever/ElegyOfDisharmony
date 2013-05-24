using EquestriEngine.Data;
using EquestriEngine.Data.Inputs.Interfaces;
using EquestriEngine.Data.Inputs;
using EquestriEngine.Objects.Scenes;
using EquestriEngine.Systems;
using Helper = Microsoft.Xna.Framework.MathHelper;

namespace EquestriEngine
{
    public delegate void TriggerEvent(Objects.Scenes.Node trigger, NodeFlags triggerFlags);
    public delegate void GenericEvent(object sender, IEventInput input);
    public delegate bool ConditionalEvent(object sender, IEventInput input);

    public static class EngineGlobals
    {
        #region Switches And Variables

        public static void ToggleSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.ToggleSwitch(sInput.Input);
        }

        public static void TurnOnSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.TurnOnSwitch(sInput.Input);
        }

        public static void TurnOffSwitch(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
            Systems.DataManager.TurnOffSwitch(sInput.Input);
        }

        public static void ModifyVariables(object sender, IEventInput input)
        {
            if (input is IntModInput)
            {
                var temp = (IntModInput)input;
                switch (temp.AdjustmentType)
                {
                    case AdjustType.Addition:
                        temp.VarInput += temp.IntInput;
                        break;
                    case AdjustType.Subtract:
                        temp.VarInput -= temp.IntInput;
                        break;
                    case AdjustType.Multiply:
                        temp.VarInput *= temp.IntInput;
                        break;
                    case AdjustType.Divide:
                        temp.VarInput /= temp.IntInput;
                        break;
                    case AdjustType.Modulus:
                        temp.VarInput %= temp.IntInput;
                        break;
                    case AdjustType.Set:
                        Variable.Set(temp.VarInput, temp.IntInput);
                        break;
                    case AdjustType.SetToGold:
                        Variable.Set(temp.VarInput, Systems.DataManager.GetVariable("{gold}").AsInt);
                        break;
                    case AdjustType.SetToPlayerX:
                        break;
                    case AdjustType.SetToPlayerY:
                        break;
                }
            }
            else if (input is VariableModInput)
            {
                var temp = (VariableModInput)input;
                switch (temp.AdjustmentType)
                {
                    case AdjustType.Addition:
                        temp.InputL.AsInt += temp.InputR.AsInt;
                        break;
                    case AdjustType.Subtract:
                        temp.InputL.AsInt -= temp.InputR.AsInt;
                        break;
                    case AdjustType.Multiply:
                        temp.InputL.AsInt *= temp.InputR.AsInt;
                        break;
                    case AdjustType.Divide:
                        temp.InputL.AsInt /= temp.InputR.AsInt;
                        break;
                    case AdjustType.Modulus:
                        temp.InputL.AsInt %= temp.InputR.AsInt;
                        break;
                    case AdjustType.Set:
                        temp.InputL.Value =  temp.InputR.Value;
                        break;
                }
            }
        }

        #endregion

        #region Conditionals

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Condition(object sender, IEventInput input)
        {
            if (!(input is ConditionInput))
            {
                ConsoleWindow.WriteLine("Invalid input passed to Condition method");
            }
            
            var cInput = (ConditionInput)input;
            var v1 = cInput.InputA as Variable;
            var v2 = cInput.InputB as Variable;
            switch (cInput.Comparison)
            {
                case CompareType.Var_Var:
                case CompareType.Var_Gold:
                    if (cInput.CheckValue == CheckValue.ValueEqual)
                        return v1.Value == v2.Value;
                    else if (cInput.CheckValue == CheckValue.ValueGreater)
                        return v1.AsInt > v2.AsInt;
                    else if (cInput.CheckValue == CheckValue.ValueGreaterEqual)
                        return v1.AsInt >= v2.AsInt;
                    else if (cInput.CheckValue == CheckValue.ValueLess)
                        return v1.AsInt < v2.AsInt;
                    else if (cInput.CheckValue == CheckValue.ValueLessEqual)
                        return v1.AsInt <= v2.AsInt;
                    break;
                case CompareType.Var_String:
                    if (cInput.CheckValue == CheckValue.ValueEqual)
                        return v1.Value == v2.Value;
                    break;
                case CompareType.Switch_Val:
                    break;
            }

            return false;
        }

        #endregion

        #region Game Related

        public static void AdjustGold(object sender, IEventInput input)
        {
            var gold = DataManager.GetVariable("{gold}");
            IntInput temp = new IntInput();
            if (input is IntInput)
                temp = (IntInput)input;
            else
                EquestriEngine.ErrorMessage = "Invalid input passed into Adjust Gold";

            if (sender is Achievement || sender is Objects.GameObjects.Player)
            {
                new SystemWidgets.GoldDisplay(gold.AsInt, temp.Input);
            }
 
            gold.AsInt += temp.Input;
        }

        /// <summary>
        /// Starts a battle sequence
        /// </summary>
        /// <param name="sender">The object that sent the request </param>
        /// <param name="input">A BattleInput, this will hold information on enemies in battle, and spoils</param>
        public static void InitiateBattle(object sender, IEventInput input)
        {

        }

        #endregion

        public static void AddCutSceneActor(object sender, IEventInput input)
        {

        }

        public static void ShowMessageBox(object sender, IEventInput input)
        {
            var sInput = (StringInput)input;
        }

        public static bool ShowDialogueChoice(object sender, IEventInput input)
        {
            return false;
        }

        #region Audio

        public static void ChangeMusic(object sender, IEventInput input)
        {
            //var aInput = input as AudioInput;
        }

        #endregion

        #region Methods

        public static Data.Inputs.MethodParamPair GenerateMethodFromString(string input)
        {
            Data.Inputs.MethodParamPair output = null;
            string[] temp = input.Split(';');
            string methodName = temp[0].ToLower();
            switch (methodName)
            {
                case "addgold":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        try
                        {
                            GenericEvent method = AdjustGold;
                            IntInput parameter = new IntInput();
                            parameter.Input = int.Parse(temp[1]);
                            output = new MethodParamPair(method,null, parameter);
                        }
                        catch
                        {
                            EquestriEngine.ErrorMessage = "Error Creating MethodParamPair";
                        }
                        break;
                    }
                case "addexp":
                    {
                        GenericEvent method = AdjustGold;
                        IntInput parameter = new IntInput();
                        parameter.Input = int.Parse(temp[1]);
                        output = new MethodParamPair(method,null, parameter);
                        break;
                    }
                case "toggleswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        GenericEvent method = ToggleSwitch;
                        StringInput parameter = new StringInput();
                        parameter.Input = temp[1];
                        output = new MethodParamPair(method,null, parameter);
                        break;
                    }
                case "activateswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);
                        break;
                    }
                case "deactivateswitch":
                    {
                        if (temp.Length != 2)
                            throw new Data.Exceptions.EngineException("Incorrect number of parameters passed", false);

                        break;
                    }
                default:
                    throw new Data.Exceptions.EngineException("Method name Not Found", false);
            }
            return output;
        }

        #endregion
    }

    public static class MathHelper
    {
        public const float E = Helper.E;
        public const float Log10E = Helper.Log10E;
        public const float Log2E = Helper.Log2E;
        public const float Pi = Helper.Pi;
        public const float PiOver2 = Helper.PiOver2;
        public const float PiOver4 = Helper.PiOver4;
        public const float TwoPi = Helper.TwoPi;

        public static float Barycentric(float v1, float v2, float v3, float a1, float a2)
        {
            return Helper.Barycentric(v1, v2, v3, a1, a2);
        }

        public static float CatmullRom(float v1, float v2, float v3, float v4, float amount)
        {
            return Helper.CatmullRom(v1, v2, v3, v4, amount);
        }

        public static float Clamp(float val, float min, float max)
        {
            return Helper.Clamp(val, min, max);
        }

        public static float Distance(float v1, float v2)
        {
            return Helper.Distance(v1, v2);
        }

        public static float Slerp(float v1, float v2, float amount)
        {
            return Helper.SmoothStep(v1, v2, amount);
        }

        public static float ToRadians(float degrees)
        {
            return degrees * (Pi / 180);
        }
    }

    public class Equestribatch : Microsoft.Xna.Framework.Graphics.SpriteBatch
    {
        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        public Equestribatch(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
            : base(device)
        {
            _ready = true;
        }
    }
}
