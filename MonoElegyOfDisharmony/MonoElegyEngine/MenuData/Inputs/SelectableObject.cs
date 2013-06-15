using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquestriEngine.MenuData.Inputs
{
    public delegate void SelectAction(object sender, SelectItemArgs e);

    public abstract class SelectableObject : MenuObject
    {
        private bool 
            _enabled,
            _hover,
            _selected;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Is the control currently being hovered over
        /// </summary>
        public bool IsHovered
        {
            get { return _hover; }
            set 
            { 
                _hover = value;
                if (value)
                {
                    if (OnHover != null)
                        OnHover(this.OnHover, new SelectItemArgs() { Hovering = _hover, Selected = _selected });
                }
                else
                {
                    if (OnEndHover != null)
                        OnEndHover(this.OnHover, new SelectItemArgs() { Hovering = _hover, Selected = _selected });
                }
            }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (value && _selected != value)
                {
                    if(OnSelected != null)
                        OnSelected(this, new SelectItemArgs() { Hovering = IsHovered, Selected = value });
                }
                _selected = value;
            }
        }

        public event SelectAction 
            OnSelected,
            OnHover,
            OnEndHover;
    }
}
