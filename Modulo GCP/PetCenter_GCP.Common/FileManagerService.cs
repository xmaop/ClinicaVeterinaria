using System;
using System.Drawing;
using System.IO;
using Omu.Drawing;
using System.Configuration;

namespace PetCenter_GCP.Common
{
    public class FileManagerService
    {
        private const string Path = "\\";
        private const string TempPath = "\\";

        public FileManagerService()
        {
        }

        public void DeleteImages(string root, string filename)
        {
            try
            {
                var c = root + Path;
                File.Delete(c + filename);
            }
            catch (Exception)
            {
            }
        }

        public void DeleteImageFolder(string root)
        {
        }

        public string CopyImages(string root, string filename, string temproot)
        {
            var t = temproot + TempPath + filename;
            var r = root + Path + filename;
            var g = Guid.NewGuid() + ".jpg";

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            var newfilename = string.Empty;

            if (File.Exists(t))
            {
                while (File.Exists(r))
                {
                    g = Guid.NewGuid() + ".jpg";
                    filename = g;
                    r = root + Path + filename;
                }

                File.Copy(t, r);
                newfilename = filename;
            }
            else
            {
                if (File.Exists(r))
                {
                    return filename;
                }
            }


            return newfilename;
        }

        public string CopyFiles(string root, string filename, string temproot, bool isNewfilename = false)
        {
            string t = temproot + TempPath + filename;
            string r = r = root + Path + filename;
            string extension = System.IO.Path.GetExtension(filename).ToLower();

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            var newfilename = string.Empty;

            if (File.Exists(t))
            {
                if (isNewfilename)
                    while (File.Exists(r))
                    {
                        string g = Guid.NewGuid() + extension;
                        filename = g;
                        r = root + Path + filename;
                    }

                File.Copy(t, r);
                newfilename = filename;
            }
            else
            {
                if (File.Exists(r))
                {
                    return filename;
                }
            }

            return newfilename;
        }

        public void CopyStream(Stream input, string filePath)
        {
            //byte[] buffer = new byte[8 * 1024];
            //int len;
            //while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    output.Write(buffer, 0, len);
            //}
            var buffer = new byte[input.Length];
            input.Read(buffer, 0, buffer.Length);
            System.IO.File.WriteAllBytes(filePath, buffer);
        }

        public static void CopyStream(Stream input, Stream output)
        {
            input.Seek(0, SeekOrigin.Begin);
            input.CopyTo(output);
        }

        public string SaveTempFile(string root, Stream inputStream, string fileName)
        {
            var filePath = root + TempPath + fileName;

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            using (FileStream outputStream = File.OpenWrite(filePath))
            {
                CopyStream(inputStream, outputStream);
            }

            return fileName;
        }

        public string SaveTempFile(string root, Stream inputStream)
        {
            return SaveTempFile(root, inputStream, Guid.NewGuid().ToString());
        }

        public string SaveTempFileGrid(string root, Stream inputStream, string extension)
        {
            return SaveTempFileGrid(root, inputStream, Guid.NewGuid().ToString(), extension);
        }

        public string SaveTempFileGrid(string root, Stream inputStream, string fileName, string extension)
        {
            var filePath = root + TempPath + fileName + extension;

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            using (FileStream outputStream = File.OpenWrite(filePath))
            {
                CopyStream(inputStream, outputStream);
            }

            return fileName;
        }
    }
}
