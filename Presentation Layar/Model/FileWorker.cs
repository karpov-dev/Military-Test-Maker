using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Presentation_Layar.Model
{
    static class FileWorker
    {
        public static string RemoveToRoot(string file)
        {
            if ( file != null )
            {
                string folderName = @"\ImageResources";
                string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string FileDirectory = rootDirectory + folderName;
                DirectoryInfo dir = new DirectoryInfo(FileDirectory);
                FileInfo fileInf = new FileInfo(file);
                string newPath = FileDirectory + @"\" + fileInf.Name;
                if ( !dir.Exists )
                {
                    dir.Create();
                }
                if ( fileInf.Exists )
                {
                    fileInf.CopyTo(newPath, true);
                }
                return newPath;
            }
            return null;
        }

        public static bool FileExists(string path)
        {
            FileInfo file = new FileInfo(path);
            if ( file.Exists ) return true;
            return false;
        }

        public static bool IsImage(string path)
        {
            List<string> images = new List<string>();
            images.Add(".png");
            images.Add(".jpg");
            for ( int i = 0; i < images.Count; i++ )
            {
                if ( path != null )
                {
                    if ( path.Contains(images[i]) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string OpenFileAndGetPath(string filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*")
        {
            string fileName = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = filter;
            bool? result = dlg.ShowDialog();
            if ( result == true ) fileName = dlg.FileName;
            return fileName;
        }
    }
}
