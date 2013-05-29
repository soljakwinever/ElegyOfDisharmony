using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquestriEngine.MenuData.Inputs
{
    public delegate void SelectAction(object sender, SelectItemArgs e);

    public class SelectableObject : MenuObject
    {
        public override void Update(float dt)
        {
            throw new NotImplementedException();
        }

        public override void Draw(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
