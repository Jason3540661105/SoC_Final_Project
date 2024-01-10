using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game
{
    class space : Picture
    {
        public space(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.space;
        }
    }
}
