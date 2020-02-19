using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Model
{
    public class Music
    {

        private string title;
        private string filePath;
        private int index;
        private int length;

        public string Title { get => title;}
        public string FilePath { get => filePath;  }
        public int Index { get => index;  }
        public int Length { get => length; set => length = value; }


        public Music(string filePath, string title, int index)
        {
            this.filePath = filePath;
            this.title = title;
            this.index = index;
        }

        public void PullingIndex()
        {
            index -= 1;
        }

        public override string ToString()
        {
            return Title;
        }

    }
}
