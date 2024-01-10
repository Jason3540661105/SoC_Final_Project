using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game
{
    class box_on_target : Picture
    {
        public box_on_target(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.box_on_target;
        }
    }
}
