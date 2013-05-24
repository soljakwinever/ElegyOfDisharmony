using EquestriEngine.Data.Scenes;
using EquestriEngine.Objects.Scenes;
using EquestriEngine.Data;

namespace EquestriEngine.Objects.GameObjects
{
    public enum ControlType
    {
        DoubleJump,
        Fly,
        Hover,
    }

    public class Player : Scenes.Node
    {
        private static Data.Controls.IControlScheme _controlReference;

        private ControlType _controlType;

        private bool _jumping;

        SceneObjectNode _body;

        Variable stepsTaken;

        Vector3 playerVelocity;

        private const float PLAYER_SPEED = 0.1f;

        public Player()
            : base("{player}", Vector3.Zero)
        {
           // _pupils = new SceneObjectNode("player_eyes", "fluttershy_eye", new Vector3(2,0,0));
            //this.AddNode(_pupils);

            stepsTaken = Systems.DataManager.GetVariable("{steps_taken}");
        }

        public void LoadContent()
        {
            _body = new SceneObjectNode("player_body", "fluttershy_body", Vector3.Zero);
            this.AddNode(_body);
        }

        public void Update(float dt)
        {
            if (_controlReference != null)
            {
                /*timer += dt;
                if (_controlReference.X != 0 && timer > 0.2f)
                {
                    timer = 0;
                }
                else
                {
                    if (timer > 0.2f)
                        timer = 0.2f;
                }*/

                if (_controlReference.X != 0 || _controlReference.Y != 0)
                {
                    this.playerVelocity = new Vector3(_controlReference.X * PLAYER_SPEED, playerVelocity.Y, _controlReference.Y * PLAYER_SPEED);
                    stepsTaken.AsInt++;
                }
                else
                    this.playerVelocity *= new Vector3(0.75f, 1, 0.75f);
                this.Position += playerVelocity;
                if (!_jumping && _controlReference.Input2())
                {
                    _jumping = true;
                    playerVelocity.Y = 0.25f;
                }
                if (Position.Y != 0)
                {
                    playerVelocity.Y -= 0.015f;
                    if (Position.Y <= 0)
                    {
                        Position = new Vector3(Position.X, 0, Position.Z);
                        _jumping = false;
                    }
                }
            }

        }

        public void RegisterControlReference(Data.Controls.IControlScheme controls)
        {
            _controlReference = controls;
        }

        public void UnregisterControlReference()
        {
            _controlReference = null;
        }
    }
}
