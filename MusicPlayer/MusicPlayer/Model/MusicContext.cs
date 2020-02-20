using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Model
{
    class MusicContext: DbContext
    {
        public MusicContext() : base("master")
        {

        }

        public DbSet<Music> Musics { get; set; }
    }
}
