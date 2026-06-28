using System;

namespace Vibra.DomainModel.Entities
{
    public class PlaylistSong
    {
        public Guid PlaylistId { get; set; }
        public Guid SongId { get; set; }
        public int Order { get; set; }

        public virtual Playlist Playlist { get; set; }
        public virtual Song Song { get; set; }
    }
}
