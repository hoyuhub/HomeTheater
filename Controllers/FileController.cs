using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeTheater.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using HomeTheater.EF;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace HomeTheater.Controllers
{
    public class FileController : Controller
    {
        private string path = @"d:\MusicFiles";

        public void PathTest()
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }

        [HttpPost]
        public IActionResult InsertSong()
        {
            int result = 0;
            string msg = "�Ѵ��ڸ�����";
            try
            {
                PathTest();
                var files = Request.Form.Files;

                foreach (var file in files)
                {
                    var filename = ContentDispositionHeaderValue
                                   .Parse(file.ContentDisposition)
                                  .FileName
                                    .Trim('"');

                    string[] strArray = filename.Split('-');
                    string fullPath = path + "\\" + filename;
                    string songName = strArray[0];
                    string singer = strArray[1].Substring(0, strArray[1].IndexOf("."));

                    if (!CheckSong(songName, singer))
                    {
                        msg = string.Format("{0}{1},", msg, songName);
                        continue;
                    }

                    Music m = new Music(Guid.NewGuid().ToString(), songName, singer, fullPath);

                    using (var cx = new MyContext())
                    {
                        cx.Musics.Add(m);
                        result += cx.SaveChanges();
                    }

                    using (FileStream fs = System.IO.File.Create(fullPath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }

            }
            catch (Exception e)
            {
                result = -1;
                msg = "�����쳣";
                throw e;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("result", result.ToString());
            dic.Add("msg", msg);
            return Json(dic);
        }

        public bool CheckSong(string name, string singer)
        {
            int result = 0;
            using (var cx = new MyContext())
            {
                result = cx.Musics.Count(d => d.Name == name && d.Singer == singer);
            }
            if (result > 0)
                return true;
            else
                return false;
        }

    }
}
