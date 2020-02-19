using MusicPlayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModel
{
    public enum Status
    {
        Play, Pause, Stop
    }

    struct PlayMode 
    {
        public const int NEXT = 1;
        public const int PREV = -1;
    }

    public class MusicViewModel
    {

        #region 변수
        private List<Music> list = new List<Music>();
        private MediaControl media = MediaControl.Instance;
        #endregion

        public int curMusicIndex { get; private set; }
        public Status Status { get; private set; }

        public MusicViewModel()
        {
            curMusicIndex = 0;
            Status = Status.Stop;
        }


        public void AddMusic(string filePath, string title)
        {
            Music music = new Music(filePath, title, list.Capacity);
            list.Add(music);
        }

        public Music PlayMusic(int index, bool loop)
        {
            if (media.Status() == "playing" || media.Status() == "paused")
            {
                media.Close();
            }
            
            media.Open(list[index < 0 ? index = 0 : index > list.Capacity ? list.Capacity : index].FilePath);

            media.Play(loop);

            Status = Status.Play;
            list[index].Length = media.Length();
            return list[index];
        }

        public Music PlayMusic(int index, bool loop, int seekTime)
        {
            if (media.Status() == "playing" || media.Status() == "paused")
            {
                media.Close();
            }

            media.Open(list[index < 0 ? index = 0 : index > list.Capacity-1 ? list.Capacity-1 : index].FilePath);

            media.Play(loop, seekTime);

            list[index].Length = media.Length();
            Status = Status.Play;
            return list[index];
        }

        public void DeleteMusic(int index)
        {
            for(int i = index;i<list.Count; i++)
            {
                list[i].PullingIndex();
            }
            list.RemoveAt(index);    
        }

        public void StopMusic() 
        {
            Status = Status.Stop;
            media.Stop();
        }

        public Music MusicControl(int index)
        {
            if (media.Status() == "playing")    // 재생 중일때는 일시정지
            {
                media.Pause();
                Status = Status.Pause;
            }
            else if (media.Status() == "paused" )    // 일시정지 중일 때는 다시 재생
            {
                media.Resume();
                Status = Status.Play;
            }
            else if (media.Status() == "stopped")    // 노래가 끝났거나 시작 전에는 새로 재생.
            {
                media.Close();
                return PlayMusic(curMusicIndex + 1, true);
            }
            else
            {
                if (list.Capacity > 0)
                {
                    return PlayMusic(curMusicIndex, true);
                }
                else
                {
                    media.Close();
                }
            }
            return list[curMusicIndex];

        }

        public List<Music> GetPlayList()
        {
            return list;
        }

        public void ChangeVolume(int value)
        {
            media.MasterVolume(value);
        }

        public int GetPosition()
        {
            return media.Position();
        }


        ///// <summary>
        ///// 다음 또는 이전 곡을 재생할 때 쓰이는 메소드
        ///// </summary>
        ///// <param name="position"> 이전 곡이면 -1, 다음 곡이면 1</param>
        ///// <param name="loop"></param>
        ///// <returns></returns>
        //public int PlayMusic(int position, bool loop)
        //{
        //    if (media.Status() == "playing" || media.Status() == "paused")
        //    {
        //        media.Close();
        //    }

        //    media.Open(list.SelectMusic(list.GetIndex() + position).FilePath);

        //    media.Play(loop);
        //    return media.Length();
        //}

        //public int PlayMusic(string path, bool loop)
        //{
        //    if (media.Status() == "playing" || media.Status() == "paused")
        //    {
        //        media.Close();
        //    }

        //    media.Open(path);

        //    media.Play(loop);
        //    return media.Length();
        //}

        //public int PlayMusic(string path, bool loop, int seekTime)
        //{
        //    if (media.Status() == "playing" || media.Status() == "paused")
        //    {
        //        media.Close();
        //    }

        //    media.Open(path);

        //    media.Play(loop, seekTime);
        //    return media.Length();
        //}

        ///// <summary>
        ///// 현재 상태에 따라서 음악이 재생되거나 일시 정지되거나 새로 재생이 됩니다.
        ///// -1이 반환 시 재생목록에 음악이 없는 경우입니다.
        ///// </summary>
        ///// <returns></returns>
        //public int MusicControl()
        //{
        //    if (media.Status() == "playing")    // 재생 중일때는 일시정지
        //    {
        //        status = Status.Pause;
        //        media.Pause();
        //    }
        //    else if (media.Status() == "paused")    // 일시정지 중일 때는 다시 재생
        //    {
        //        media.Resume();
        //        status = Status.Play;
        //    }
        //    else if (media.Status() == "stopped")    // 노래가 끝났거나 시작 전에는 새로 재생.
        //    {
        //        media.Close();
        //        return PlayMusic(PlayMode.NEXT, true);
        //    }
        //    else
        //    {
        //        if (list.getLenght() != 0)
        //        {
        //            return PlayMusic(PlayMode.NEXT, true);
        //        }
        //        else
        //        {
        //            media.Close();
        //        }
        //    }
        //    return -1;
        //}
        //public void StopMusic() { media.Stop(); }

        //public void AddMusic(Music music) 
        //{ 
        //    list.AddMusic(music); 
        //}
        //public void DeleteMusic(int position) 
        //{ 
        //    list.DeleteMusic(position); 
        //}

        //public void ChangeVolume(int value)
        //{
        //    media.MasterVolume(value);
        //}
        //public int GetPosition()
        //{
        //    return media.Position();
        //}
        //public Status GetStatus()
        //{
        //    return status;
        //}
    }
}
