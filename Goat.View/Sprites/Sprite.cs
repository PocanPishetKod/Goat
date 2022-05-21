using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.View.Sprites
{
    public struct Sprite : IDisposable, IAsyncDisposable
    {
        public readonly string Name;
        public readonly Stream Data;

        public Sprite(string name, Stream data)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public void Dispose()
        {
            Data?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (Data == null)
                return;

            await Data.DisposeAsync();
        }
    }
}
