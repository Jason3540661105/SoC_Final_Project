using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game
{
    class box : Picture
    {
        public box(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.box;
        }
    }
}
