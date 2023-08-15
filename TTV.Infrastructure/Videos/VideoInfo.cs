using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTV.Infrastructure.Videos
{
    public record VideoInfo
    {
        public int Id { get; set; }
        public string Filename { get; set; } = string.Empty;
        public Stream Stream { get; set; } = default!;
    }
}
