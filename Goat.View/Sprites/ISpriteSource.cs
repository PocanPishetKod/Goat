using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.View.Sprites
{
    internal interface ISpriteSource
    {
        Task<Sprite> Load(string name);
    }
}
