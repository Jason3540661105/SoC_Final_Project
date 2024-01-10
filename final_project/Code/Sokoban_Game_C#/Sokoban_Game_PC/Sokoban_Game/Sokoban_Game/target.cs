using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game
{
    class target : Picture
    {
        public target(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.target;
        }
    }
}
