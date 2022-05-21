using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Goat.View.Sprites
{
    internal class EmbededSpriteSource : ISpriteSource
    {
        private static readonly Assembly _executingAssembly;
        private static readonly IReadOnlyList<string> _embededFileNames;

        static EmbededSpriteSource()
        {
            _executingAssembly = Assembly.GetExecutingAssembly();
            _embededFileNames = _executingAssembly.GetManifestResourceNames();
        }

        public Task<Sprite> Load(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var fullFileName = GetFullFileName(name);

            if (fullFileName == null)
                throw new InvalidOperationException($"Sprite with name {name} not found");

            var stream = _executingAssembly.GetManifestResourceStream(fullFileName);

            if (stream == null)
                throw new InvalidOperationException($"Embeded file {fullFileName} not found");

            return Task.FromResult(new Sprite(name, stream));
        }

        private string? GetFullFileName(string shortName)
        {
            return _embededFileNames.FirstOrDefault(n => n.EndsWith($"{shortName}.png"));
        }
    }
}
