using System;

//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂
//                                                                                          🙂
//                        by：Twilight                                               🙂
//                                                                                          🙂
//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂

namespace Lemon_App
{
    public class Music
    {
        public Music() { }
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string MusicName { set; get; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Singer { set; get; }
        /// <summary>
        /// 用于播放的音乐ID
        /// </summary>
        public string MusicID { set; get; }
        /// <summary>
        /// 用于获取图像的ID
        /// </summary>
        public string ImageID { set; get; }
        /// <summary>
        /// 专辑名称
        /// </summary>
        public string ZJ { set; get; }
        /// <summary>
        /// SQ品质
        /// </summary>
        public string Fotmat { set; get; }
        /// <summary>
        /// HQ品质
        /// </summary>
        public string HQFOTmat { set; get; }
        /// <summary>
        /// 歌词ID
        /// </summary>
        public string GC { set; get; }
        /// <summary>
        /// mv ID
        /// </summary>
        public string MV { set; get; }
        /// <summary>
        /// 是否为排行榜歌单
        /// </summary>
        public Boolean IsDF { set; get; }
        /// <summary>
        /// 排行榜歌单的标准品质uri
        /// </summary>
        public string DFSONGURI { set; get; }
        /// <summary>
        /// 排行榜歌HQ
        /// </summary>
        public string DFSONGURI_HQ { set; get; }
        /// <summary>
        /// 特殊类型歌曲C4L0
        /// </summary>
        public bool SL_128 { set; get; }
        /// <summary>
        /// 特殊类型歌曲C4L0 ID
        /// </summary>
        public string SL_128ID { set; get; }
    }

}
