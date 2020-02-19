//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MusicPlayer.Model
//{
//    public class PlayList
//    {
//        private List<Music> list = new List<Music>();
//        private int index = 0;

//        public void AddMusic(Music music)
//        {
//            list.Add(music);
//        }

//        public void DeleteMusic(int index)
//        {
//            list.RemoveAt(index);
//        }

//        public Music CurMusic()
//        {
//            Music curMusic = (Music)from music in list
//                          where music.Index == this.index
//                          select music;
//            return curMusic;
//        }

//        public Music SelectMusic(int index)
//        {
//            Music selectMusic = (Music)from music in list
//                                    where music.Index == index
//                                    select music;
//            this.index = selectMusic.Index;
//            return selectMusic;
//        }

//        public int GetIndex()
//        {
//            return index;
//        }

//        public int getLenght()
//        {
//            return list.Capacity;
//        }
//    }
//}
