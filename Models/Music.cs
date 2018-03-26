using System;
namespace HomeTheater.Models
{
    public class Music
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Singer { get; set; }
        public string SongPath { get; set; }

        public Music(){}
        public Music(string id,string name,string singer,string songPath)
        {
            this.id=id;
            this.Name=name;
            this.Singer=singer;
            this.SongPath=songPath;
        }
    }

}