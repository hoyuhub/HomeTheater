using System;
namespace HomeTheater.Models
{
    public class User
    {
        public string  id { get; set; }

        public string  LoginName { get; set; }

        public string  Pwd { get; set; }
        //此字段用于记录用户选歌列表
        public string  JsonList { get; set; }
    }
}