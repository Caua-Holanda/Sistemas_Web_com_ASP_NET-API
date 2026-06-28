using DomainModel;
using System;
using System.Collections.Generic;

namespace Vibra.DomainModel.Entities
{
    public class Song : EntityBase
    {
        public Song()
        {
            PlaylistSongs = new HashSet<PlaylistSong>();
            FavoriteSongs = new HashSet<FavoriteSong>();
        }

        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string AudioUrl { get; set; }
        public int TrackNumber { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
        public virtual ICollection<FavoriteSong> FavoriteSongs { get; set; }
    }
}
